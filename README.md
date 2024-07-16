# Publish the ASP.NET Core App to IIS

## Prerequisites

- [.NET Core SDK](https://learn.microsoft.com/en-us/dotnet/core/sdk) installed on the development machine.
- Windows Server configured with the **Web Server (IIS)** server role. If your server isn't configured to host websites with IIS, follow the guidance in the _IIS configuration_ section of the [Host ASP.NET Core on Windows with IIS](https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-6.0&tabs=netcore-cli) article and then return to this tutorial.

## Install the .NET Core Hosting Bundle

Install the _.NET Core Hosting Bundle_ on the IIS server. The bundle installs the .NET Core Runtime, .NET Core Library, and the [ASP.NET Core Module](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-6.0). The module allows ASP.NET Core apps to run behind IIS.

Download the installer using the following link:

[.NET Core Hosting Bundle installer (direct download)](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.32-windows-hosting-bundle-installer)

1.  Run the installer on the IIS server.
2.  Restart the server or execute `net stop was /y` followed by `net start w3svc` in a command shell.

## Create the IIS site

1.  On the IIS server, create a folder to contain the app's published folders and files. In a following step, the folder's path is provided to IIS as the physical path to the app. For more information on an app's deployment folder and file layout, see [ASP.NET Core directory structure](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/directory-structure?view=aspnetcore-5.0).
2.  In IIS Manager, open the server's node in the **Connections** panel. Right-click the **Sites** folder. Select **Add Website** from the contextual menu.
3.  Provide a **Site name** and set the **Physical path** to the app's deployment folder that you created. Provide the **Binding** configuration using (`https://*:443/` and `https://+:443`) and create the website by selecting **OK**.
4.  Confirm the process model identity has the proper permissions.

    If the default identity of the app pool (**Process Model** > **Identity**) is changed from `ApplicationPoolIdentity` to another identity, verify that the new identity has the required permissions to access the app's folder, database, and other required resources. For example, the app pool requires read and write access to folders where the app reads and writes files.

# Publication

We need to publish and copy the current application to the **Physical path** that we selected and configured during the creation of IIS Web Site.

> **Note:** Before starting to publish, this directory must exist.

## Publish the Host

You can automatically publish and copy the host by running the Power Shell Script in the path _$'{solution directory path}\build'_:

```
.\build-with-iss
```

Next, the current prompt should appear:

```
Input your absolute physical directory path to publish:
```

Then, we must type the _defined_ **Physical path**. After that we can request the host locally in [Swagger Definition](https://localhost/swagger/index.html).

> **Note:** We need to refresh or restart IIS Manager.

## Update a publication

Delete optionally the content of the _current_ **Physical path** and **re-run** the Power Shell Script:

```
.\build-with-iss
```

Now, introduce the _same_ or a _new_ **Physical path** previously selected and configured during the creation of IIS Web Site.

> **Note:** We need to refresh or restart IIS Manager.
