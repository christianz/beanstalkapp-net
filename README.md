beanstalkapp-net
=====

Beanstalk RESTful API wrapper written in C#. Follows the conventions outlined in the [Official Ruby gem](https://github.com/isabanin/beanstalkapp)

[See the official API for more information](http://api.beanstalkapp.com)

## Installation

For now, grab the source code and compile it yourself (it's very quick).

## License

See MIT-LICENSE.

## Dependencies

Uses and includes the [Json.NET](http://json.codeplex.com/) library.

## Usage

First enable API access in your Beanstalk account by going to Account > Setup. Only the account owner can do that.

Reference beanstalkapp_net.dll in your project and initialize the wrapper like this:

    Beanstalk.Initialize("username", "password", "your_beanstalk_subdomain");

Then you can start using the API:

    var accounts = Account.Find();
 
For information about all API methods please go to the official documentation site:

http://api.beanstalkapp.com

Copyright (c) 2012 Christian Zachariasen, io7 Software.
Released under the MIT license