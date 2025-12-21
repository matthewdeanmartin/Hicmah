## Project Description
A nuget installable web hit counter with self hosting and intranet apps in mind. Includes data collector and a data analytics dashboard.

## Beta Code as of Aug 30, 2012
The hit counter, dashboard and nuget package are all at the Beta stage. Many things work and the installation process isn't too bad. But it isn't really production quality code yet.

With Nuget, the hicmah counter can be installed into the default ASP.NET webforms template in under a minute.

## Steps to set up demo

1) You already have nuget https://nuget.org/, right?
2) Go get this package https://nuget.org/packages/hicmah in the gallery or search using the VS add in.
3) Optionally, read the readme (and all the documentation http://hicmah.codeplex.com/documentation)
4) Launch the sample website. Navigate to the \Hicmah\ folder.
5) If you have SQL Express installed, the application will step you through it's best efforts to create the database and optionally create some sample data.
6) Optionally update the masterpage to include the jquery plug in to collect client side stats.

## What
It is a hit counter.

Hicmah is optimized for an intranet, private website, custom web applications with ongoing maintenance and development. It provides the same sort of features as Google Analytics, except Google Analytics is heavily optimized for ecommerce and content publishing, global audience, public website, search engine and google adwords driven application.

It records a variety of information extract from an HTTP request about the page, user, server, time, user's computer and then records the hit in as little time and space as possible.

Hicmah is not a general purpose error logger, nor a debug or trace logger, nor a non-repudiatable security logger.

## How
It is easy to try out and can be integrated into an application without modifying existing code. In the simplest scenario, the dll is dropped into a folder and the web.config is modified to choose a data source, the handler and module.

## Why
Google Analytics might be good enough, but it often isn't available internally or organization rules preclude using a cloud service that could leak confidential information to other organizations, such as Google.

IIS error log parsers are also good, but again, organization rules often preclude access to the IIS log by maintenance developers, especially on shared servers.

## Features
See specification in the documentation section for features, both existing and planned.

## Easy Licensing
Hicmah dual licensed with the MIT license and MSPL license so it is absolutely compatible with those two licenses and probably compatible with most other license as well. It is copyfree and GPL compatible. So you should legally be able to use it in your closed source, commercial, private application. You can use it in your GPL, public open source applications, too.