SeleniumTesting
===============

Using Selenium and Nunit to test Deployment Manager

Selenium - http://www.seleniumhq.org/
Nunit - http://www.nunit.org/

You will need Microsoft Visual Studio 2012 to run the code.

You will also need to install Nunit.Runners - which is available as a NuGet package, which you can get by Tools- Library Package Manager - Manage Nuget Packages from solution and then selecting Online and typing Nunit.Runners in the search field and clicking install.

Change the debug settings for the project, by right clicking on the SmokeTests project and selecting Debug.  Select the start Action - start external program and point at the location of the nunit-x86.exe which should be something like C:\Users\username\Documents\GitHub\SeleniumTesting\SmokeTestsForDM\packages\NUnit.Runners.2.6.2\tools\nunit-x86.exe and then add RedGate.Deploy.SmokeTests.dll to the commandline arguments field.

You will also need to enable Nuget package restore, which can be done by Selecting Tools- Library Package Manager - Package Manager Settings and then selecting the box for Allow NuGet to download missing packages during build.

Deployment Manager - http://www.red-gate.com/delivery/deployment-manager/
Starter edition is free for up to 5 projects and deployment targets
To run the tests, you will need Deployment Manager installed on the same machine on the default port (or you will need to change  the port number in the code on SmokeTestBase.cs).  You will also need to create two users (via Settings - Users - Create a new user) AdminUser and NonAdmin both with the password Password.  Make AdminUser an administrator via the Settings - Administrators -Add administrator.  These accounts are used on used on SiteUser.cs and AllPagesTest.cs respectively.



