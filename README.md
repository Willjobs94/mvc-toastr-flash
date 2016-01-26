# MVC Toastr Flash

This package adds Rails-like flash functionality to your MVC controllers, backed by the excellent [Toastr](http://codeseven.github.io/toastr/) JavaScript notification library. Flash notifications are persisted through redirects and are removed when shown.

## Install

Best way to install is via [Nuget](https://nuget.org):

```
PM> Install-Package RedWillow.MvcToastrFlash
```

This package has the following NuGet dependencies:

* [Microsoft.AspNet.Mvc](https://www.nuget.org/packages/Microsoft.AspNet.Mvc), version >= 3.0.20105.1
* [toastr](https://www.nuget.org/packages/toastr), version >= 2.1.1

## Usage

First, follow the steps required to get Toastr up and working: https://www.nuget.org/packages/toastr

### Layouts / Views

At the top of your layouts pages, or views that don't use layouts (that you want Toastr notifications to appear on) add:

```C#
@using RedWillow.MvcToastrFlash
```

And following the `@RenderSection("scripts", required: false)` near the bottom add (should be right before the closing `body` tag):

```C#
@Html.ToastrNotifications()
```

### Controllers

Add this `using` statement:

```C#
using RedWillow.MvcToastrFlash;
```

Then, flash notifications to your heart's content:

```C#
public ActionResult Index()
{
    this.Flash(Toastr.SUCCESS, "Welcome!", "Glad you arrived safely.");

    return View();
}
```

See the Sample project for more usage examples.

## License

The MVC Toastr Flash package is released under a [MIT license](http://opensource.org/licenses/MIT)