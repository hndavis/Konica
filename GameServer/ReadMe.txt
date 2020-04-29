ReadMe.txt

Welcome to my version of Line Intersect Game

it is a c# implementation using websockets.

I have chosen to use these external libraries and source code:
	log4net, 
	WebSocketSharp, 
	Autofac, 
	adapted code from martin-thoma.com

All 3 party code code and libaries are free to use and distribute.  
All the libraries are availibe with NuGet which will make maintance and developemnt painless ( as far as libraries)


One main consideration was to build the system with testing, robustness, and minimum of interdependancies in mind.

	I choose log4net because i want to have flexible and configurable system for logging.  
This allows changing of log level and targets ( file, sql/table, port..).  It has standard llo and feel for logging .

	I choose WebSocketSharp to handle the web socket connection because it handles many case of communication that 
I might have to handle if web sockets class were used directly.

	In order to less the interdependace of the class I wanted to use dependancy  injection.  This also will 
allow Mock up classes to run standard tests.  The standard test can be run when new code is committed to the main 
source control branch to assure continance of functionality.   I choose Autofac because it seems to be a "light" weight
container that can be used in different areas of dot net.

    The class i used from martin-thoma has a robust method of determining line intersection.


In all i looked at this project as foundation or beginings for a comercial product so it may seem over engineered 
for just this Technical Assesment.  

This "server" runs as a console application, which is appropriate for the technical assement.  It could be easly adapted 
to run as a service.. or run on linux as a mono application.








