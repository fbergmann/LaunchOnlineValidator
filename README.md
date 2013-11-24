## Launch SBML Online Validator
This project hosts a simple web automation project, that enables SBW modules to send SBML models over to the SBML Online Validator. This is definitely convenient: 

![Launch SBML Online Validator](https://raw.github.com/fbergmann/LaunchOnlineValidator/master/images/LaunchOnlineValidator.png)

If you launch the executable directly, it will open the SBML Online Validator. And if you launch the executable with an SBML file, that file will be send to the Validator. 

Downloads are as always hosted by SourceForge, the current version is: 

[http://sourceforge.net/projects/sbw/files/modules/LaunchOnlineValidator/SetupSBMLValidator_1.0.exe/download](http://sourceforge.net/projects/sbw/files/modules/LaunchOnlineValidator/SetupSBMLValidator_1.0.exe/download)

## Implementation 
Well to be honest, this project really just has a couple of lines of code. All the magic is actually leveraged by a nuget package `WatiN.2.1.0`. And whilte theoretically it would support a number of browsers, I could only get IE to work reliably for now. Then the magic happens in just one function: 

        public static void ValidateSBML(string sbml)
        {
            var browser = new IE("http://sbml.org/validator");
            browser.TextField(Find.ByName("rawSBML")).Value =
                            sbml;

            var link = 
                browser.
                    Links.FirstOrDefault(s => s.Url.Contains("fromPaste"));
            if (link != null)
                link.Click();
        }

## License 
This project is open source and freely available under the [Simplified BSD](http://opensource.org/licenses/BSD-2-Clause) license. Should that license not meet your needs, please contact me. 

Copyright (c) 2013, Frank T. Bergmann  
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.