AutoImageTurner und AutoImageTurnInCSharp
===========================================
Rotating images based on the 'orientation' EXIF tag with JHEAD software.

AutoImageTurnInCSharp: Kind of a 'library' for CSharp  on how to rotate images based on the 'orientation' EXIF tag.

AutoImageTurner is the corresponding .exe.

[See here](https://msdn.microsoft.com/en-us/library/windows/desktop/ms534418(v=vs.85).aspx) for more information on the EXIF tags used in Windows (.Net).

For C# see also [here](https://github.com/SeppPenner/RotateImagesInCSharp) for another possible implementation.

Example images using each of the EXIF orientation flags (1-to-8), in both landscape and portrait orientations [See here](https://github.com/recurser/exif-orientation-examples).

[See here](http://www.daveperrett.com/articles/2012/07/28/exif-orientation-handling-is-a-ghetto/) for more information.


[![Build status](https://ci.appveyor.com/api/projects/status/bpp3st995erveceh?svg=true)](https://ci.appveyor.com/project/SeppPenner/autoimageturner)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/AutoImageTurner.svg)](https://github.com/SeppPenner/AutoImageTurner/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/AutoImageTurner.svg)](https://github.com/SeppPenner/AutoImageTurner/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/AutoImageTurner.svg)](https://github.com/SeppPenner/AutoImageTurner/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/SeppPenner/AutoImageTurner/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/AutoImageTurner/badge.svg)](https://snyk.io/test/github/SeppPenner/AutoImageTurner)
[![Blogger](https://img.shields.io/badge/Follow_me_on-blogger-orange)](https://franzhuber23.blogspot.de/)
[![Patreon](https://img.shields.io/badge/Patreon-F96854?logo=patreon&logoColor=white)](https://patreon.com/SeppPennerOpenSourceDevelopment)
[![PayPal](https://img.shields.io/badge/PayPal-00457C?logo=paypal&logoColor=white)](https://paypal.me/th070795)


## Basic usage
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

## Screenshot from the executable
![Screenshot from the executable](https://github.com/SeppPenner/AutoImageTurner/blob/master/AutoImageTurner-Screenshot.PNG "Screenshot from the executable")

Copyright (of the images)
-------------------------

Dave Perrett :: hello@daveperrett.com :: [@daveperrett](http://twitter.com/daveperrett)

These images are licensed under the [MIT License](http://opensource.org/licenses/MIT).

Copyright (c) 2010 Dave Perrett. See [License](https://github.com/recurser/exif-orientation-examples/blob/master/LICENSE) for details.

Change history
--------------

See the [Changelog](https://github.com/SeppPenner/AutoImageTurner/blob/master/Changelog.md).