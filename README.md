AutoImageTurner und AutoImageTurnInCSharp
===========================================
Rotating images based on the 'orientation' EXIF tag with JHEAD software

AutoImageTurnInCSharp: Kind of a 'library' for CSharp  on how to rotate images based on the 'orientation' EXIF tag.

AutoImageTurner is the corresponding .exe.

[See here](https://msdn.microsoft.com/en-us/library/windows/desktop/ms534418(v=vs.85).aspx) for more information on the EXIF tags used in Windows (.Net).

For C# see also [here](https://github.com/SeppPenner/RotateImagesInCSharp) for another possible implementation.

Example images using each of the EXIF orientation flags (1-to-8), in both landscape and portrait orientations [See here](https://github.com/recurser/exif-orientation-examples).

[See here](http://www.daveperrett.com/articles/2012/07/28/exif-orientation-handling-is-a-ghetto/) for more information.


The assembly/ exe was written and tested in .Net 4.8.

[![Build status](https://ci.appveyor.com/api/projects/status/bpp3st995erveceh?svg=true)](https://ci.appveyor.com/project/SeppPenner/autoimageturner)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/AutoImageTurner.svg)](https://github.com/SeppPenner/AutoImageTurner/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/AutoImageTurner.svg)](https://github.com/SeppPenner/AutoImageTurner/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/AutoImageTurner.svg)](https://github.com/SeppPenner/AutoImageTurner/stargazers)
[![GitHub license](https://img.shields.io/badge/license-AGPL-blue.svg)](https://raw.githubusercontent.com/SeppPenner/AutoImageTurner/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/AutoImageTurner/badge.svg)](https://snyk.io/test/github/SeppPenner/AutoImageTurner)


## Basic usage:
```csharp
using System;
using System.Windows.Forms;

namespace AutoImageTurner
{
    public class AutoTurnExample
    {
        public void Test()
        {
            try
            {
                IAutoTurnImages rotator = new AutoTurnImages();
                rotator.RotateImagesInFolder("C:\\abc", "jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
		
		public void Test()
        {
            try
            {
                IAutoTurnImages rotator = new AutoTurnImages();
                rotator.RotateImagesInFolderNoMessage("C:\\abc", "jpg");
                //Doesn't show an error message from the method itself
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
```

## Screenshot from the executable:
![Screenshot from the executable](https://github.com/SeppPenner/AutoImageTurner/blob/master/AutoImageTurner-Screenshot.PNG "Screenshot from the executable")

Change history
--------------

* **Version 1.0.1.0 (2019-10-13)** : Updated nuget packages, added GitVersionTask.
* **Version 1.0.0.5 (2019-05-06)** : Updated .Net version to 4.8.
* **Version 1.0.0.4 (2017-03-24)** : Updated Languages.dll to version 1.0.0.4.
* **Version 1.0.0.3 (2017-03-21)** : Updated Languages.dll.
* **Version 1.0.0.2 (2017-03-11)** : Switched to .Net 4.6.2, added license and refactored code.
* **Version 1.0.0.1 (2016-12-03)** : Added basic usage to Readme.
* **Version 1.0.0.1 (2016-08-27)** : Added "all image files" selection to the exe and the possibility to not neccessarily get error messages in the library
* **Version 1.0.0.0 (2016-08-12)** : 1.0 release.

Copyright (of the images)
-------------------------

Dave Perrett :: hello@daveperrett.com :: [@daveperrett](http://twitter.com/daveperrett)

These images are licensed under the [MIT License](http://opensource.org/licenses/MIT).

Copyright (c) 2010 Dave Perrett. See [License](https://github.com/recurser/exif-orientation-examples/blob/master/LICENSE) for details.
