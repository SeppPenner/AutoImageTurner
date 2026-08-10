# Project rules for Claude

## What this is

AutoImageTurner is a small Windows Forms desktop application. The user picks a folder and a file
extension, and the program rotates the images in that folder according to their EXIF `orientation`
tag. The rotation itself is not implemented here, it is done by the bundled command line tools
`jhead.exe` (reads the tag, drives the rotation) and `jpegtran.exe` (the lossless JPEG transform
that jhead calls). The UI is bilingual (German, English) and switchable at runtime through a combo
box. Distribution happens as an Inno Setup installer, not as a NuGet package.

One solution `src/AutoImageTurner.sln` with exactly two projects,
`src/AutoImageTurner/AutoImageTurner.csproj` (the application) and
`src/AutoImageTurner.Tests/AutoImageTurner.Tests.csproj` (MSTest, added in version 1.0.10.0).
Project, assembly, namespace and the form class are all called `AutoImageTurner`, so
`AutoImageTurner.AutoImageTurner` is the form. That is intentional, do not rename one of them.

Layout inside `src/AutoImageTurner`:

- `Program.cs`: entry point, `[STAThread]`, `Application.Run(new AutoImageTurner())`.
- `AutoImageTurner.cs`: the form logic. Folder dialog, extension combo box, language handling, the
  error dialogs and the decision which rotation method is called.
- `AutoImageTurner.Designer.cs` plus `AutoImageTurner.resx`: Windows Forms designer output. The
  window icon is embedded in the `.resx`. Designer code is generated, it does not follow the hand
  written conventions below, do not reformat it by hand.
- `AutoTurnImages.cs` plus `IAutoTurnImages.cs`: the two rotation methods, one with a finished
  message box and one without. This is the only place that starts an external process, keep new
  process handling here.
- `GlobalUsings.cs`: all usings of the project.
- `languages/de-DE.xml` and `languages/en-US.xml`: the UI texts.
- `RotateImage.ico`: application and installer icon. `License.txt`: shipped next to the executable.
- `jhead.exe` and `jpegtran.exe`: the rotation tools, copied to the output directory with
  `CopyToOutputDirectory=Always` and shipped by the installer. `.gitignore` excludes `*.exe`, so
  both are tracked through the two negation rules at the end of that file. Without them in the
  repository a fresh clone builds an application that cannot rotate anything, and the tests fail.

Layout inside `src/AutoImageTurner.Tests`:

- `AutoTurnImagesTests.cs`: the rotation of an image carrying an orientation tag, an image without
  such a tag, a folder without a matching file, a file whose extension only starts with the given
  format, a second run on an already rotated image and a folder that does not exist. The variant
  `RotateImagesInFolder` is not covered, it ends in a `MessageBox` that would block the test run.
- `TestData/Rotate90.jpg` and `TestData/NoOrientation.jpg`: two generated 120 x 80 images, the first
  one with an EXIF orientation tag of 6, the second one without any orientation tag. Both are
  copied to the output directory with `CopyToOutputDirectory=Always` and are marked binary in
  `.gitattributes`.
- `GlobalUsings.cs`: all usings of the test project.

The test project has `UseWindowsForms` enabled because the class under test shows message boxes, and
it needs no copy rule for `jhead.exe`, `jpegtran.exe` and the language files. Those arrive in its
output directory through the project reference.

Translation comes from the NuGet package
[HaemmerElectronics.SeppPenner.Language](https://www.nuget.org/packages/HaemmerElectronics.SeppPenner.Language/)
(assembly and namespace `Languages`, source in the sibling repository `CSharpLanguageManager`).
Its runtime contract is convention based and this project depends on it:

- `LanguageManager` loads every `*.xml` from a `languages` directory beside the executing assembly.
- Each file deserializes into `Identifier`, `Name` and `Words/Word/Key` plus `Value`. The
  identifier must be a culture name that `CultureInfo` understands (`de-DE`, `en-US`).
- `GetWord` returns `null` for an unknown key, it does not throw and it does not fall back to
  another language. A new UI text therefore has to be added to **both** language files, otherwise
  one language silently shows an empty string.
- The two XML files are copied to the output directory with `CopyToOutputDirectory=Always`, the
  same holds for `License.txt`. Removing that is what makes the shipped program start without any
  texts.

## Build

```powershell
dotnet build src/AutoImageTurner.sln
```

```powershell
dotnet test src/AutoImageTurner.sln
```

- Single target framework `net10.0-windows`, `WinExe`, `UseWindowsForms`, `RuntimeIdentifiers`
  `win-x64`. This is a Windows only application, unlike the language library it references. The test
  project targets `net10.0-windows` as well.
- All build properties live directly in the two `.csproj` files and are duplicated there. There is
  no `Directory.Build.props` in this repository.
- `TreatWarningsAsErrors` is enabled, so every warning breaks the build, NuGet warnings (`NU****`)
  from restore included. A clean build reports zero warnings, keep it that way.
- `NU1803` (HTTP source usage during restore) is the one warning suppressed via `NoWarn`. Fix
  warnings instead of extending that list. `NuGetAudit` and `NuGetAuditMode=all` are on, so a
  vulnerable transitive package fails the build too.
- Versions come from GitVersion.MsBuild out of the git tags, for example `1.0.10-1` for the first
  commit after tag `1.0.9`. Never edit a version property or an assembly version by hand.
- Restore needs nuget.org. If a private feed is configured globally on the machine and answers 404
  for public packages, restore fails with `NU1301`. Then build with an explicit source:
  `dotnet build src/AutoImageTurner.sln --source https://api.nuget.org/v3/index.json`.
- `Setup/build-setup-files.bat` deletes all `bin` and `obj` folders below `src`, then runs
  `dotnet publish -c Release -r win-x64 --self-contained true -o bin/publish` and removes the
  `*.pdb` files from the publish output. The installer ships the whole runtime, so the target machine
  needs no .NET installation, at the price of roughly 120 MB of publish output. The batch file does
  **not** run the Inno Setup compiler, that is a separate manual step.
- Tests are MSTest, in the single test project `src/AutoImageTurner.Tests`, which follows the same
  package set as the sibling repositories: `Microsoft.NET.Test.Sdk`, `MSTest.TestAdapter`,
  `MSTest.TestFramework`, `coverlet.collector` and `GitVersion.MsBuild`. `dotnet test` runs 6 tests,
  they need no network and no fixture outside the repository, but they do start the real
  `jhead.exe`. Each test copies its image into its own directory below `Path.GetTempPath()` and
  deletes it afterwards, so a test run leaves the working tree untouched. Never claim a test run
  happened without running it.
- Beyond the tests, a behaviour change of the UI is verified by starting the built executable and
  letting it rotate a real image whose EXIF orientation tag is not 1.

## Code conventions

Follow the surrounding code, it is consistent throughout the hand written files:

- File header comment block with `<copyright file="..." company="Hämmer Electronics">` and a
  `<summary>`, then the file-scoped namespace `AutoImageTurner`.
- XML doc comments on every type and every member, private members and event handlers included, no
  exceptions. Implementations of an interface member additionally carry `<inheritdoc cref="..."/>`
  and `<seealso cref="..."/>` pointing at that interface.
- `Nullable`, `ImplicitUsings` and `LangVersion latest` are enabled.
- New `using` directives go into `src/AutoImageTurner/GlobalUsings.cs`, inside the existing
  `#pragma warning disable IDE0065` block, never at the top of a file. The editorconfig requires
  usings inside the namespace (`csharp_using_directive_placement=inside_namespace:warning`), which
  global usings cannot satisfy, that is what the pragma is for. Do not add other pragmas. The
  comment text in that block is German because Visual Studio generated it, leave it alone.
- Fields, properties, methods and events are always accessed with `this.` qualification
  (`dotnet_style_qualification_for_*` at severity `warning`).
- `src/.editorconfig` also enforces braces everywhere, no multiple blank lines, four spaces, CRLF,
  UTF-8, file scoped namespaces, `System` usings sorted first and `IDE0005` as warning. Analyzer
  warnings are fixed, not silenced.
- The form is split into small single purpose private methods (`InitializeLanguageManager`,
  `LoadLanguagesToCombo`, `StartAllowed`, `RotateImagesNormal`, ...). Keep new logic in that shape
  instead of growing one big handler.

## Known quirks

Do not silently "clean up" these, they are existing behaviour:

- **The extension combo box is the data source of the logic.** Its entries are hard coded English
  strings in the designer (`All images`, `bmp`, `gif`, `giff`, `jfif`, `jpe`, `jpeg`, `jpg`, `png`,
  `tif`, `tiff`), they are never translated, and `ButtonStartClick` compares against the literal
  `"All images"`. The designer text is therefore a functional constant, not a caption.
- **`All images` relies on the alphabetical order.** For that entry the form loops over every combo
  item, skips `All images` itself and calls the silent rotation for each extension, except for
  `tiff`, which goes through the variant that shows the finished message box. Because the combo box
  has `Sorted = true`, `tiff` is the last item, so the user gets exactly one message at the end.
  Removing `tiff` from the list or adding an extension after it silently removes or moves that
  message.
- **Only one caption is translated at runtime.** `OnLanguageChanged` sets the text of the folder
  button and nothing else. The `Start` key exists in both language files but is never read, the
  start button keeps its designer text.
- **The language init order.** `SetCurrentLanguage("de-DE")` in `InitializeLanguageManager` runs
  before the event handler is subscribed, so it updates no caption. What actually applies a
  language for the first time is `LoadLanguagesToCombo` with `SelectedIndex = 0`, which fires the
  combo box event. The language shown at startup is whichever file ends up first in the combo box,
  which is `de-DE.xml` only because of the alphabetical file order.
- **`InitializeLanguageManager` also creates the rotator.** `this.rotator = new AutoTurnImages(...)`
  sits in that method because the rotator needs the language manager. Every call site therefore
  checks `this.rotator is null` first, which keeps the nullable analysis quiet.
- **The window title** is `Application.ProductName` plus `Application.ProductVersion`, and
  `ProductVersion` is the GitVersion informational version. On an untagged commit the title reads
  something like `AutoImageTurner 1.0.10-1+Branch.master.Sha.0f08e33...`. Only a tagged build shows
  a clean version.
- **jhead needs jpegtran beside it.** `jhead -autorot` shells out to `jpegtran` for the lossless
  rotation, so `jpegtran.exe` has to be findable through `PATH` when jhead runs with the image
  folder as its working directory. Both tools live next to the executable, so the process that
  starts jhead has to put that directory on the child `PATH`. Without it jhead exits with code 1,
  writes `Error : Problem executing specified command` plus the shell message about the unknown
  command `jpegtran`, and leaves the file untouched.
- **jhead is JPEG only, and its exit code decides what the user sees.** The rotation is a JPEG
  feature, the entries `bmp`, `gif`, `giff`, `png`, `tif` and `tiff` of the combo box can never be
  rotated. jhead answers a pattern without any match with `Error: No files matched` and exit code 1,
  which is why `AutoTurnImages` skips a folder without a matching file beforehand. It answers a file
  that is no JPEG with `Not JPEG: <name>` on standard error, and it exits with code 1 only when it
  could process **no** file at all. A folder holding one real JPEG plus one broken one therefore
  ends with exit code 0 and the good file rotated. Selecting a non-JPEG format in a folder that
  really holds such files ends in the error dialog, that is intended, before version 1.0.10.0 the
  application silently claimed success.
- **`.gitignore` excludes `*.exe` and `[Bb]in`**, yet `Setup/AutoImageTurner-Setup.exe` is tracked.
  It was added with `git add -f` and has to be updated the same way for every release. The two
  rotation tools are tracked through negation rules instead, the installer is not, because it
  changes with every release and a negation rule would make it show up as a modification all the
  time.
- **The `.csproj` lists the two language files twice** in its `None Update` item group. MSBuild
  does not care, the duplication is harmless.
- **The `README.md` headline mentions a project `AutoImageTurnInCSharp`** that does not exist in
  this repository. What it means is the `AutoTurnImages` class inside the application, there is no
  second project and no library package.
- **AppVeyor badge without CI in the repository.** `README.md` links an AppVeyor build that is
  configured outside of this repository. There is no `.github` folder and no pipeline file here.
- **`src/AutoImageTurner.sln.DotSettings`** is tracked and holds nothing but a ReSharper user
  dictionary (`autorot`, `H_00E4mmer`, `jhead`, `Kindsof`, `rotator`). Leave it alone.
- **`.gitattributes` sets `* text=auto`** and every rule of the Visual Studio template below it is
  commented out. `*.exe binary` and `*.jpg binary` were added at the end so that git never
  normalizes line endings inside the tools, the installer or the test images. Any further binary
  file needs its own rule.

## Releasing

The tag comes **before** the installer build, never after. GitVersion derives the assembly version
from the tag, so an installer compiled on an untagged commit contains an executable that reports
something like `1.0.10-4+Branch.master.Sha...` in its window title instead of a clean `1.0.10`.

1. Make the change.
2. Add an entry at the top of `Changelog.md` in the existing format:
   `* **Version 1.0.10.0 (2026-08-10)** : Short description.`
3. Bump `MyAppVersion` in `Setup/AutoImageTurner-Setup.iss` to the same version, four parts.
4. Commit that, then tag the commit with the plain version number, no `v` prefix (`1.0.9`,
   `1.0.8`, ...). The existing tags are lightweight tags, create new ones the same way.
5. Run `Setup/build-setup-files.bat`, it publishes the tagged commit to
   `src/AutoImageTurner/bin/publish`.
6. Compile `Setup/AutoImageTurner-Setup.iss` with Inno Setup, it writes
   `Setup/AutoImageTurner-Setup.exe`.
7. Commit that file with `git add -f`, then push the commits and the tag. This last commit sits
   after the tag, the same way `Updated setup.` sits after tag `1.0.9`.

Never run the publish or the installer build unless explicitly asked to release.

There is no CI configuration in this repository, no `.github` folder and no publish pipeline. There
is no `Updating.md` and no `HowToUse.md` here, the `README.md` with the screenshot
(`AutoImageTurner-Screenshot.PNG`) is the only user documentation.

## Git

- **Never amend a commit.** No `git commit --amend`, not for a typo in the message, not to add a
  forgotten file, not even when the commit is still local. Write a follow-up commit instead. The
  release versions come from tags on exact commits, an amended commit leaves its tag pointing at a
  commit that no longer exists in the branch.

## Writing style

- Commit messages are written **in English only**: short, precise subject line, explanatory body
  when needed.
- Code comments and comments in project files such as `.csproj` are **always English**, regardless
  of the language used in the conversation.
- **No em dashes or en dashes** (`—`, `–`), neither in prose, commit messages, code comments nor
  documentation. Use a regular hyphen, comma, colon, parentheses or a separate sentence.
- German texts (documentation, chat replies, the `de-DE.xml` values) always use real umlauts and ß,
  never ASCII transliterations such as `ae`, `oe`, `ue` or `ss`. Identifiers, file names and
  configuration keys stay unchanged where umlauts are technically undesirable.
