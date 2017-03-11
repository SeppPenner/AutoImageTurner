# AutoImageTurner und AutoImageTurnInCSharp
===============
Rotating images based on the 'orientation' EXIF tag with JHEAD software

AutoImageTurnInCSharp: Kind of a 'library' for CSharp  on how to rotate images based on the 'orientation' EXIF tag.

AutoImageTurner is the corresponding .exe.

[See here](https://msdn.microsoft.com/en-us/library/windows/desktop/ms534418(v=vs.85).aspx) for more information on the EXIF tags used in Windows (.Net).

For C# see also [here](https://github.com/SeppPenner/RotateImagesInCSharp) for another possible implementation.

Example images using each of the EXIF orientation flags (1-to-8), in both landscape and portrait orientations [See here](https://github.com/recurser/exif-orientation-examples).

[See here](http://www.daveperrett.com/articles/2012/07/28/exif-orientation-handling-is-a-ghetto/) for more information.


The assembly/ exe was written and tested in .Net 4.6.2.

[![Build status](https://ci.appveyor.com/api/projects/status/bpp3st995erveceh?svg=true)](https://ci.appveyor.com/project/SeppPenner/autoimageturner)


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

Change history
--------------

* **Version 1.0.0.2 (2017-03-11)** : Switched to .Net 4.6.2, added license and refactored code.
* **Version 1.0.0.1 (2016-12-03)** : Added basic usage to Readme.
* **Version 1.0.0.1 (2016-08-27)** : Added "all image files" selection to the exe and the possibility to not neccessarily get error messages in the library
* **Version 1.0.0.0 (2016-08-12)** : 1.0 release.

Copyright (of the images)
-------------------------

Dave Perrett :: hello@daveperrett.com :: [@daveperrett](http://twitter.com/daveperrett)

These images are licensed under the [MIT License](http://opensource.org/licenses/MIT).

Copyright (c) 2010 Dave Perrett. See [License](https://github.com/recurser/exif-orientation-examples/blob/master/LICENSE) for details.
