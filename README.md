QaaWS Samples
=============

This is a sample project written using Visual Studio 2010 that demonstrates the use of ETO Results Query as a Web Service platform.

Requirements
-------------

* Visual Studio 2010
* Microsoft .NET 4

Projects
-------------

### SiteList

A console application that executes a QaaWS against the Standard Reference Universe to obtain a list of sites.

After downloading the source code and compiling, run SiteList.exe from the command line.

* QaaWS URL: https://liveoffice.etosoftware.com/dswsbobje/qaawsservices/biws?WSDL=1&cuid=Afg7hriNfChBslO9TDgBqLc
* Demonstrates basic use of QaaWS

### ParticipantList

A console application that executes a QaaWS against the Standard Participant Universe to obtain a list of participants in a specified site.

After downloading the source code and compiling, run ParticipantList.exe from the command line. The application accepts Site as a parameter on the command line or will present the user with a list and prompt for input.

* QaaWS URL: https://liveoffice.etosoftware.com/dswsbobje/qaawsservices/biws?WSDL=1&cuid=AbCz8IZ1FRxJgxT3v7b0XYA
* Demonstrates basic use of QaaWS, a List of Values (LOV) and supplying prompt values in a request.