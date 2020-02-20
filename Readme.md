# Tutorial-Bouncing-Ball

A simple tutorial for making a cross platform C# based application that does very basic 2D physics for circles.

## Introduction

In this repository you'll find a complete and working application that simulates basic 2D physics for bouncing balls.  
If you follow this readme, you'll learn how to implement this yourself.

This is intended for beginners, so feel free to skip ahead if you are feeling confident.

## Getting Started

If you don't have one already, make a github.com account.  
They're free, so we'll use it as a neat place to store your work on this tutorial.

### Dependencies

You'll need to download some dependencies first so that you can develop in C#.  
**If you're using a Raspberry Pi**, these dependencies will already be installed.

* [git for windows](https://git-scm.com/download/win)  
   This will be used to put your work into github.
* [dotnet core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)  
   You want the latest version of the SDK (Software Development Kit).
* [Visual Studio Code](https://code.visualstudio.com/)  
   This is the editor we'll be using. Feel free to use any other editor you prefer.
* [Avalonia dotnet templates](https://github.com/AvaloniaUI/avalonia-dotnet-templates)  
   Avalonia is a cross platform port of WPF - a user interface that we'll use to build our application.      
   Clone the repository: `git clone https://github.com/AvaloniaUI/avalonia-dotnet-templates.git`  
   Install the templates: `dotnet new --install ./avalonia-dotnet-templates`

### Starting your Project

The first thing we're going to need to do is to create the C# project that will build the application.  
Start by opening a shell.

> Throughout this tutorial I will refer to a 'shell', by that I mean either bash (on the Raspberry Pi's) or PowerShell (on Windows).  
> Windows: Open the start menu. Type "PowerShell". Run the application.  
> R Pi: Open the start menu. Go to "Accessories". Click "Terminal".

In the shell, navigate to a directory that you'd like to create the project folder within. For example, your home folder or documents folder.

> Windows: `cd "$env:USERPROFILE/Documents"`  
> R Pi: `cd ~`

Now, we can use the dotnet command line interface to create a project.

> `dotnet new avalonia.app -n Physics`

Now, navigate into the new directory we just created with the above command.

> `cd ./Physics`

Finally, we can initialize git in this directory.

> `git init`

Right, that's it for the command line for a while. We can launch code and open our project!

> Windows: `code .`  
> R Pi: `code-oss .`

## Saving your Work

Throughout this tutorial, you can use github as a place to store your own work.  
When you are at a point that you want to save, start by configuring git so that it knows who you are:

1. Open a shell (or use the one you opened earlier)
2. Enter the commands:  
   `git config user.email = "my.email@address"`
   `git config user.name = "My Name"`

Excellent, now, we need to have a repository in github to push to:

1. Open [github](github.com)
2. Create a new repository with a sensible name.

You'll be greeted with a screen that has an explanation of how to push your first code.  
There are two sets of instructions, what we're interested in doing is pushing our existing code up to github.

For example, if I create a test repository github tells me to:

```sh
git remote add origin https://github.com/Alexei-B/test.git
git push -u origin master
```

For now, just copy the `git remote add` bit.

1. Open a shell (or use the one you opened earlier)
2. Use the `cd` (change directory) command to navigate to your directory (if you aren't there already).  
   * Windows: `cd "$env:USERPROFILE/Documents/Physics"`
   * R Pi: `cd ~/Physics`
3. Add the remote.

Awesome, that's all of the first time setup done.  
You can push your work up to github easily through VS Code now:

1. Save all your files.
2. Open the source control panel (Ctrl+Shift+G).
3. Stage all of your changes (hit the plus button next to the files).
4. Write a message that summarises the change in the field above ("Added stuff", "Changed whatever").
5. Click the tick at the top of the panel.
6. Click the synchronise button in the bottom left.

Any of that confusing?  
[Instructions with pictures here.](https://code.visualstudio.com/docs/editor/versioncontrol)

## Building and Debugging

Now would be an excellent time to check that your application builds and runs.  
Within the VS Code we just opened, you should see your new files on the left.

### The Template

We're going to learn about the code over time, so don't worry if you can't take all of this in at once. I've provided a basic run-down of what we just created below, but you don't need to worry if parts of it don't click yet. It's better to keep moving on and come back to anything that didn't make sense later. The nice thing about all these files is that it's a bunch of boring busy work that the tools have just done for you, so you don't have to totally understand all of it just yet.

|     File Name     | Description |
|-------------------|-------------|
|.gitignore         |This file tells git which files should not be considered source code. For example, the Avalonia binaries.|
|App.xaml           |This file is the template for our application, in XML. This configures the theme that the user interface uses, for example.|
|App.xaml.cs        |This file contains the behaviour for the App.xaml template. This is where we initialize our components and our window.|
|MainWindow.xaml    |This is the template for our application window. This is what you actually see on the display. This is the file that we'll edit to add our own visual components.|
|MainWindow.xaml.cs |This is the code responsible for the behaviour of the main window. If we want a button, we'll hook up events (like being clicked) here.|
|nuget.config       |This file configures the package manager which downloads things like Avalonia for you.|
|Physics.csproj     |This is the project file. This is what the dotnet CLI uses to figure out what packages to download and what files to build.|
|Program.cs         |This is the entry point of the application. Program.Main is the first function run when you launch the application.|

I'll bring up files again when they become relevant. Let the information sink in rather than trying to memorise the table above.

### Building

#### Windows

You can build your project directly through VS Code. Just hit F5.  
You may get a prompt asking you what runtime to use, select '.NET Core' if you do.  
If you don't see '.NET Core' as an option, select 'more...' and then install the C# extension and try again.

A new folder (.vscode) and two new files (tasks.json and launch.json) will be generated for you.  
These files configure what VS Code will do to build and launch the application.  
The generated information should be correct, so you shouldn't need to modify them.  
Once they are generated, you may have to hit F5 again to actually start the application.

If your application built and ran, you should see a window with the text *"Welcome to Avalonia!"*.

#### Raspberry Pi

Unfortunately, there isn't a working debugger for dotnet just yet that builds on the ARM processor that the Raspberry Pi uses. So, for the Pi, we'll need to rely on building and running from the command line. This isn't ideal, but you'll still be able to create the application from this tutorial.

At the bottom of the VS Code window you can pull up a terminal. Alternatively, you can use the terminal you've got open already.

> `dotnet build`  
> `dotnet run`

## Writing our First Code!

OK, that's enough setup. We can finally write some code.

> If you're feeling confident, go google the Avalonia documentation and get started. If this is easy enough for you to work with, consider reading [the tutorial that I used for the collision physics](https://www.gamasutra.com/view/feature/131424/pool_hall_lessons_fast_accurate_.php) and just using this project as a guide for when you get stuck.

> On the other hand, if this seems sufficiently challenging: Follow this tutorial. Make mistakes. Learn from them. Do not give up.

> If this is the first code you've ever written in your life, and this is too hard, don't feel bad! [Go get started learning about C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/hello-world-your-first-program?tabs=windows#elements-of-a-c-program) and come back when you're more comfortable.

### Goal 1: Draw Circles in our App

#### PhysicsCanvas

To bounce some balls around, we need some balls to bounce. In the [Avanlonia Tutorials](https://avaloniaui.net/docs/tutorial/) you can figure out a few ways to draw circles. The method that I recommend (and that I will use in the tutorial) is to create our own custom element that inherits from the `Canvas` element. We'll call this a `PhysicsCanvas`.

1. Create a new file called `PhysicsCanvas.cs` in VS Code.
2. Write in the following code:

```CSharp
// These 'using' statements tell the compiler about external packages and behaviour we're going to pull in and depend on.
using Avalonia.Controls;
using Avalonia.Media;

// In C#, it's best practice to put your code into name spaces so that
// if someone else made a "PhysicsCanvas" then it wouldn't interfere with your definition of a PhysicsCanvas.
namespace Physics
{
    // All code in C# is writing within 'classes'.
    // A class is the definition of an object, what it can do, and all of its members.
    // This class seems rather boring, because it's getting all of it's behaviour from Canvas.
    // It's doing that using 'inheritance' (the ': Canvas' bit).
    // That is, PhysicsCanvas can do everything Canvas can do, and has every member that Canvas has.
    // This is a way for us to extend behaviour, rather than having to implement it from scratch.
    public class PhysicsCanvas : Canvas
    {
        // This is our only method in this class.
        // This method is called when the application wants to render the content of this canvas.
        public override void Render(DrawingContext context)
        {
            // For now, we'll just pass to the 'base' class that we inherited from (Canvas).
            base.Render(context);
        }
    }
}
```

I've left comments throughout the code above that describe why that code exists. However, inheritance and object oriented programming are a gigantic topic. You don't need to know anything more than what's above to write this code. So, at another time, google around for more information on OOP / inheritance to understand it more in depth.

#### PhysicsCanvas in the MainWindow

Right, we've got a `PhysicsCanvas` class. Now, we need to use this in our `MainWindow` so that when we add circles to the canvas, we'll see them. Let's go to `MainWindow.xaml` and add our new element.

Here's the XML that I used in the main window:

```XML
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:physics="clr-namespace:Physics;assembly=Physics"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="400"
        x:Class="Physics.MainWindow"
        Title="Physics">
    <!--
       This grid is going to contain all the top level elements in our main window.
       Using a grid lets us layout multiple elements, rather than just having one.
    -->
    <Grid>
        <!-- These definitions are specifying how wide and tall the rows and columns should be. -->
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="1*" />
        </Grid.ColumnDefinitions>

        <Grid.RowDefinitions>
            <RowDefinition Height="1*" />
        </Grid.RowDefinitions>

        <!-- Here's our custom element! -->
        <physics:PhysicsCanvas Grid.Column="0" Grid.Row="0" />
    </Grid>
</Window>
```

There are two key points above that make this work:

1. The `xmlns:physics` XML namespace declaration in the `Window` element attributes that specifies which assembly we're using for the `physics` namespace.
2. The `physics:` prefix on the `PhysicsCanvas` element, using the namespace we defined for the window.

> Build and run again now.  
> The message that was there should be gone, but otherwise everything should just look blank in the window.

#### Where's muh circles at?

We're not actually rendering anything in physics canvas yet, so let's hard code a circle in just to check it's working. Go to your `PhysicsCanvas.cs` file again, and add to the `Render` method.

```CSharp
        public override void Render(DrawingContext context)
        {
            // Draw
            context.DrawGeometry(
                // ...solid aqua...
                new SolidColorBrush(Colors.Aqua),
                // ...with no outline...
                null,
                // ...an ellipse that is
                new EllipseGeometry(new Rect(
                    // 50 pixels away from the top and the left borders
                    50, 50,
                    // 100 pixels wide and high.
                    100, 100
                ))
            );

            base.Render(context);
        }
```

> Build and run again now.

#### Build Errors

OK, so that build failed. If you're on Windows, you likely saw a red underline for the class `Rect` which the compiler build error message will say doesn't exist. This (like most errors) is easy to fix. It's important to see what this error looks like and to read it, so that you have a chance to deal with them in the future.

**Whenever you have a build fail: Stop. Read the error message. *Do not panic.* You've probably done something wrong. Don't worry, we all do it. Ask for help only *after* you have read the error and considered what you've done since you last had a working build.**

The fix here is to add a new using statement, to import the namespace that `Rect` is within. How do you know where that is? The [documentation tells you](https://avaloniaui.net/api/Avalonia/Rect/) ("Namespace" on the left hand side of the page).

```CSharp
// Put me at the top of the PhysicsCanvas.cs file.
using Avalonia;
```

> Build and run again now.  
> You should see an aqua circle in the top left of the window.

### Goal 2: Respond to Events

OK, we've got a circle, but we'd like some interactivity.  
Let's add a new circle when ever the user clicks the canvas.

> You could at this point decide to design your application differently, or make changes down the line. For example, you could have an 'Add Circle' button, or a cannon that launches circles into the space. It's up to you to figure that out if you chose to do that, but feel free to ask people for help.

#### Breaking down the Problem

We want to bind to the `click` event of the `PhysicsCanvas` that is within the `MainWindow`.  
How do we do that?

Here's some pseudo code (code that doesn't work and won't compile) that gets the idea across:

```CSharp
// MainWindow.xaml.cs
    public MainWindow()
    {
        InitializeComponent();
        PhysicsCanvas canvas = GetElementById("an id we can find");

        When (MainWindow is Clicked)
        {
            canvas.AddBall(event.Position);
        }
    }
```

```XML
<!-- MainWindow.xaml -->
    <physics:PhysicsCanvas id="an id we can find">
```

This is a skeleton of what we'd like to do that I'd normally write along my way to solving this kind of a problem.  
It's not real code, but this is a good way to get past writers block when writing code.

As you write the code, the interface may even suggest the correct code to write sometimes. But, even if it doesn't, doing this gives you a good idea of what you're going to need to read in the documentation to figure out the problem. In short, this tells you what you don't know yet.

#### Binding to an Event

Some of the above code isn't wildly far from what we'll do. We want to handle this magic `click` event. In Avalonia, this event is created for us and it's called `PointerPressed`.  
We also know that we need to get at our physics canvas so we can add a ball to it. Avalonia adds a way of doing that too with the `Find` method.  
Lastly, we know that `Find` (from it's documentation) looks up elements by their `x:Name` attribute.

Let's set that tag in the xaml file:

```XML
<physics:PhysicsCanvas x:Name="PhysicsCanvas" Grid.Column="0" Grid.Row="0" />
```

Now, let's make a member to store that in, in the `MainCanvas` class and assign to it on initialization in the source file for MainWindow:

```CSharp
    public class MainWindow : Window
    {
        // Here we are declaring a private member of MainWindow.
        // * Private means that nothing outside of the MainWindow can access this member.
        //   It's good practice to not expose more of a class than you need to.
        // * A member is something that exists within objects of this class.
        //   Once you make a MainWindow object from this class, it will have space to store a reference to a PhysicsCanvas.
        private PhysicsCanvas PhysicsCanvas;

        // This is the 'constructor' of the MainWindow class.
        // This is called when the MainWindow object is created from this class.
        // Classes are basically templates; objects are the concrete form of a class.
        public MainWindow()
        {
            InitializeComponent();

            // On construction, go to our xaml document, and give us a reference to the PhysicsCanvas on it.
            // The name here is arbitrary so it doesn't have to be "PhysicsCanvas", I'm just not being very creative with it.
            PhysicsCanvas = this.Find<PhysicsCanvas>("PhysicsCanvas");
        }
```

Awesome, we've got a reference to the element.  
All `public` methods and members on the object are now exposed to us, so we can call them.

Now, let's add our event.  
Events start life as a method on the class that will handle the event.  
Add this new method to the `MainWindow` class:

```CSharp
        // This is the method that will handle the mouse clicking on the main window.
        // The argument 'object sender' is the object within the window that was clicked on.
        // The argument 'PointerPressedEventArgs e' is the details of the event, such as where the click happened.
        private void MainWindow_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            // Get the click position, with respect to the position of the PhysicsCanvas element.
            Point position = e.GetPosition(PhysicsCanvas);

            // Let's just use an arbitrary radius for now.
            // If you're feeling fancy, look up the Random class in the System namespace and get a random radius.
            double radius = 50;

            // This should look similar if you remember the hard coded circle we did before.
            // However, now we are positioning this circle based on the position of the mouse and
            // the radius that we want it to be.
            var ball = new EllipseGeometry(
                new Rect(
                    position.X - radius,
                    position.Y - radius,
                    radius * 2,
                    radius * 2
                )
            );

            // This method doesn't exist... yet.
            PhysicsCanvas.AddBall(ball);
        }
```

> If you're feeling lost because I'm not quoting the entire file any more, don't forget that this repository has an example of the complete working application. Don't feel bad using it for hints, it's better to keep learning than to freeze.

This is the method that will handle the event, so, lastly, we need to *bind* the event to this method. In the constructor for `MainWindow`, add the following:

```CSharp
            PointerPressed += MainWindow_PointerPressed;
```

The operator `+=` here adds a handle to an event.  
The `PointerPressed` event is defined for us by Avalonia, and we're getting it through the `Window` class that we are inheriting from.

You'll also need to add `using` statements for the namespaces `Avalonia.Input` and `Avalonia.Media` for some of the classes we just use.

#### Adding a Ball

Last bit of the puzzle, we need that `AddBall` method in the `PhysicsCanvas` class.  
To do that, we're going to:

1. Add a new member to the `PhysicsCanvas` class that stores a list of ellipses to render.
2. Rewrite the `Render` method to loop through this list and render each ellipse.
3. Write the `AddBall` method to add to this list.

```CSharp
// For the class `List<>`
using System.Collections.Generic;

// ... I'm not repeating the whole file here, so don't remove things just because they aren't here! :)

    public class PhysicsCanvas : Canvas
    {
        // Add our list to the class.
        // Here, the angle bracket notation lets us tell the list what type (what class) it will be storing.
        // Additionally, we can immediately initialize the member.
        // The `new List<EllipseGeometry>();` code will create a new list every time a new PhysicsCanvas is created.
        private List<EllipseGeometry> Balls = new List<EllipseGeometry>();

// ...

        public override void Render(DrawingContext context)
        {
            // foreach lets us loop through Balls and select each ball from it.
            // Balls here comes from our class member.
            // var means "this is a variable, figure out the type for me please, I'm lazy and it's obvious".
            // ball is the name of the variable we'll give to each ball within the list.
            foreach (var ball in Balls) {
                // This code is executed once for each ball in the list.
                // Once we have a ball from the list, we just have to draw it same way we did before.
                context.DrawGeometry(new SolidColorBrush(Colors.Aqua), null, ball);
            }

            base.Render(context);
        }

        // Here's our new method to add the ball. Simple, right?
        public void AddBall(EllipseGeometry ball)
        {
            Balls.Add(ball);

            // Canvas gives us the method InvalidateVisual to tell it that the drawing needs to be rendered again.
            // Without this, we'd just get a blank screen.
            InvalidateVisual();
        }
```

> Build and run again now.  
> You should be able to add circles all over the window.

### Goal 3: Gravity!!!

Physics programming is quite difficult and quite fun. The mathematics that describe the mechanics for the idealized objects we're going to create are quite simple. However, writing it in code is an interesting test of your ability to break down problems into simple repeatable instructions.

Let's start simple. Making things fall.

#### Update Loop

To have real time updates to all of the balls, we're going to need the PhysicsCanvas to run some code every time we want to update them. Thankfully, Avalonia provides the `RenderLoop` class which we can add objects of type `IRenderLoopTask` to, to get them to run every frame.

`IRenderLoopTask` is an *interface*. An interface is an object oriented programming concept. When you inherit from a regular class, you get all it's members and members. When you *implement* an interface, you are saying that my class does everything required to support that interface. You have to implement all of the methods it requires. If you try to build without implementing something, the compiler will tell you what you missed.

Stating that we implement the interface is easy:

```CSharp
public class PhysicsCanvas : Canvas, IRenderLoopTask
```

Thankfully, this interface is also very easy to satisfy.

```CSharp
// This is the Avalonia code for IRenderLoopTask
// You don't want to write this, it's here just so we can read it.
namespace Avalonia.Rendering
{
    public interface IRenderLoopTask
    {
        bool NeedsUpdate { get; }

        void Render();
        void Update(TimeSpan time);
    }
}
```

It only demands two methods and one property. The most interesting of those is `Render` taking **no arguments**. We've got a method `Render` that takes **one argument**. We can't just remove that argument, because it will prevent us from being able to render circles.

Thankfully, in C# you can *overload* a method. This means multiple methods can have the same name, so long as their arguments are different.

```CSharp
        // Add this method as well, do not replace the existing Render method.
        public void Render()
        {
            // We can't actually render here, we don't have a canvas.
            // Instead, just invalidate our visual so that the other Render method gets called later.
            InvalidateVisual();
        }
```

With that out the way, the property can be added:

```CSharp
    public class PhysicsCanvas : Canvas, IRenderLoopTask
    {
        private List<EllipseGeometry> Balls = new List<EllipseGeometry>();

        // This syntax changes this form a simple member, to a full on property (which is a C# term).
        // This means that 'NeedsUpdate' has a backing variable, a get method, and set method, which
        // are all wrapped up in the name 'NeedsUpdate'.
        // Most of the time it just behaves like a normal member of the class.
        // However, it allows us to make the get public, and the set private.
        // So, anything can read this property, but it can only be set within this class.
        public bool NeedsUpdate { get; private set; } = true;

        // Let's also add these while we are here,
        // their use will become obvious when we write the Update method.
        private TimeSpan? LastUpdated { get; set; } = null;
        private static Vector Gravity = new Vector(0, 9.81);
        private const double timeFactor = 5;
```

Right, now we can implement gravity!

```CSharp
        // Update is called with an elapsed time tied to the application life-cycle.
        public void Update(TimeSpan time)
        {
            // For our purposes, we just want to know how long it was since the last frame.
            // We can the time from each frame, and then on the next frame we can compare the two to find the difference.
            // Also, we multiply by an arbitrary timeFactor here, because otherwise things just feel too slow.
            double frameTimeSeconds = (LastUpdated == null ? 0 : (time - LastUpdated)?.TotalSeconds ?? 0) * timeFactor;
            LastUpdated = time;

            // This loop will eventually calculate collisions between balls.
            // For that reason, we're going to want to use a for loop, rather than a foreach loop here.
            // A for loop requires three statements: Initialization, condition, and increment.
            //   We initialize in an index (i) to zero (our starting point).
            //   We run so long as i is less than the number of balls in our list.
            //   Each loop, add one to i to select the next ball.
            for (int i = 0; i < Balls.Count; ++i)
            {
                // Now we can access each ball using out index on the list.
                // We'll move the ball by updating it's bounding rectangle.
                Balls[i].Rect = new Rect(
                    // We're adding gravity here after multiplying it by the frame time seconds.
                    // This means that if the frame rate stutters, the animation has a good chance
                    // to still appear smooth.
                    Balls[i].Rect.TopLeft.X + Gravity.X * frameTimeSeconds,
                    Balls[i].Rect.TopLeft.Y + Gravity.Y * frameTimeSeconds,
                    Balls[i].Rect.Width,
                    Balls[i].Rect.Height
                );
            }
        }
```

Almost there!  
We need to implement the `RenderLoop` in `MainWindow` to call these new methods.

```CSharp
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Rendering;
using System;

namespace Physics
{
    public class MainWindow : Window
    {
        private PhysicsCanvas PhysicsCanvas;
        private RenderLoop RenderLoop;

// ...

        public MainWindow()
        {
            InitializeComponent();

            PhysicsCanvas = this.Find<PhysicsCanvas>("PhysicsCanvas");
            PointerPressed += MainWindow_PointerPressed;

            RenderLoop = new RenderLoop();
            RenderLoop.Add(PhysicsCanvas);
        }
```

> Build and run now.  
> You should have circles slowly moving down the screen.

### Velocity and Acceleration

This is a pretty good start! We've got animation. However, you might be a bit let down by the fact that the balls just fall at a constant rate. This is because we're just moving the balls by our Gravity vector every frame, rather than adding the Gravity vector as a *force* to the velocity of the ball, accelerating it.

Sadly, this is where `EllipseGeometry` is finally not good enough to help us. We'll need to make a `Ball` class which can remember it's velocity. Make a new file called `Ball.cs` and write the following code:

```CSharp
using Avalonia;
using Avalonia.Media;

namespace Physics
{
    // We're inheriting from EllipseGeometry,
    // so we get everything it already had.
    public class Ball : EllipseGeometry
    {
        // Let's remember our own color to make rendering more interesting.
        public Color Color { get; set; }

        // More convenient properties to think about for the motion of circles than
        // what EllipseGeometry offers.
        public Vector Center { get; private set; }
        public double Radius { get; private set; }

        // The velocity of the ball.
        public Vector Velocity { get; private set; }

        // A lazy way to do air friction.
        private static double Friction = 0.01;

        // The constructor for Ball;
        // Assigns all our new properties, then assigns to the Rect property of EllipseGeometry.
        public Ball(Vector center, double radius, Vector velocity, Color color) : base()
        {
            Center = center;
            Radius = radius;
            Velocity = velocity;
            Color = color;

            ComputeRect();
        }

        // This method will allow us to add gravity to the velocity of the ball.
        public void Gravitate(double frameTimeSeconds, Vector gravity)
        {
            Velocity = Velocity + (gravity * frameTimeSeconds);
        }

        // This method will reduce our velocity by a fraction, using our dumb air friction.
        public void ApplyFriction(double frameTimeSeconds)
        {
            Velocity = Velocity - (Velocity * Friction * frameTimeSeconds);
        }

        // Move the ball based on its current velocity.
        public void Move(double frameTimeSeconds)
        {
            Center += Velocity * frameTimeSeconds;
            ComputeRect();
        }

        // Recompute the rectangle for the EllipseGeometry.
        // We have to do this to benefit from the EllipseGeometry rendering behaviour.
        private void ComputeRect()
        {
            Rect = new Rect(
                Center.X - Radius,
                Center.Y - Radius,
                Radius * 2,
                Radius * 2
            );
        }
    }
}
```

Whoa, lots of code!

Don't panic, while this may be a large number of *lines of code*, it's all quite simple code. Splitting out your behaviour into small, well defined methods like this really helps to make reading your code easier. Sure, you'll spend longer writing code if you do it this way. But the reality is, the readability of your code matters a lot! How are you going to find bugs later on if your code is a mess? Good code readability is the toughest skill in programming. Develop your own style, there is no "one true way" to write good code. Except mine.

Now, we need to use `Ball` inside of `PhysicsCanvas`.

```CSharp
    public class PhysicsCanvas : Canvas, IRenderLoopTask
    {
        private List<Ball> Balls { get; set; } = new List<Ball>();

// ...

        public void AddBall(Ball ball)
        {
            Balls.Add(ball);
            InvalidateVisual();
        }

// ...

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            foreach (Ball ball in Balls)
            {
                context.DrawGeometry(new SolidColorBrush(ball.Color), null, ball);
            }
        }

// ...

        public void Update(TimeSpan time)
        {
            double frameTimeSeconds = (LastUpdated == null ? 0 : (time - LastUpdated)?.TotalSeconds ?? 0) * timeFactor;
            LastUpdated = time;

            for (int i = 0; i < Balls.Count; ++i)
            {
                Balls[i].Gravitate(frameTimeSeconds, Gravity);
                Balls[i].Move(frameTimeSeconds);
                Balls[i].ApplyFriction(frameTimeSeconds);
            }
        }
```

This, in turn, breaks some of the code in `MainWindow`, so we also have to update that class:

```CSharp
        private void MainWindow_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            var rng = new Random();
            Point position = e.GetPosition(PhysicsCanvas);

            var ball = new Ball(
                new Vector(position.X, position.Y),
                // Let's randomize the radius now.
                rng.NextDouble() * 100,
                // Let's also randomize the initial velocity.
                new Vector(rng.NextDouble() * 100 - 50, rng.NextDouble() * 100 - 50),
                Colors.Aqua
            );

            PhysicsCanvas.AddBall(ball);
        }
```

> Build and run now.  
> The balls should now have smooth acceleration to their animation.

### Goal 4: Walls

OK, we've got balls (check), we've got a window (check), now, let's make the balls hit the window border.  
This will be an incredibly simple collision check and response. But, it's worth covering the physics:

1. Momentum is conserved.  
   The total magnitude of all of the velocities and all of the forces going into an impact should each be the same as coming out of the impact.
2. The window has infinite mass.  
   Otherwise the balls bouncing against the window, would move the window. Feel free to try implementing that if that tickles you. I know I'd get a kick out of it.
3. `F = ma`  
   Newton says so, and he's right.

So, what happens when a ball collides with a wall? Here's a diagram:

```
|
|      __
|     /  \
|     \__/
|    /
|   /
| |_  Velocity
|
```

```
|
| __
|/  \ __________\  Normal of
|\__/           /   Impact
|    \
|     \
|      _| new Velocity
|
```

The ball impacts the wall, and the velocity is flipped around the normal of the impact.  
This happens because the wall is infinitely heavy, so it can't be moved by the ball.

The normal of the impact happens to also just be the normal of the wall in this situation.  
And the walls are totally horizontal, or totally vertical.  
This, we can just flip the horizontal or vertical component of the velocity of the ball.

Based on all this, here is the algorithm that I came up with. This is in the `Ball` class by the way:

```CSharp
        public bool ImpactBounding(double frameTimeSeconds, Rect rect) {
            bool collided = false;

            // Think about where we are going to be if we move, rather than just where we are now.
            // If we collide, instead of moving normally we will just move to the edge of the screen we hit.
            // then we will update our velocity based on our physics analysis above.
            Vector movement = Velocity * frameTimeSeconds;

            if (Rect.X + movement.X < rect.X) {
                // We've hit the left edge of the screen.
                Center = new Vector(rect.X + Radius, Center.Y);
                Velocity = new Vector(-Velocity.X, Velocity.Y);
                collided = true;
            } else if (Rect.X + Rect.Width + movement.X > rect.X + rect.Width) {
                // We've hit the right edge of the screen.
                Center = new Vector(rect.X + rect.Width - Radius, Center.Y);
                Velocity = new Vector(-Velocity.X, Velocity.Y);
                collided = true;
            }

            if (Rect.Y + movement.Y < rect.Y) {
                // We've hit the top edge of the screen.
                Center = new Vector(Center.X, rect.Y + Radius);
                Velocity = new Vector(Velocity.X, -Velocity.Y);
                collided = true;
            } else if (Rect.Y + Rect.Height + movement.Y > rect.Y + rect.Height) {
                // We've hit the bottom edge of the screen.
                Center = new Vector(Center.X, rect.Y + rect.Height - Radius);
                Velocity = new Vector(Velocity.X, -Velocity.Y);
                collided = true;
            }

            if (collided)
            {
                ComputeRect();
            }

            return collided;
        }
```

In addition, we need to add a new public property to `Ball` called `Updated`. This property will be used to remember whether or not we updated the ball this frame already. You've seen a lot of properties added now, so you can add a `public`, `bool`, called `Updated` with a `get` and a `set` to the class `Ball`.

Now, we need up update the `Update` method in the `PhysicsCanvas` class so that we can use this new impact behaviour.

```CSharp
        public void Update(TimeSpan time)
        {
            double frameTimeSeconds = (LastUpdated == null ? 0 : (time - LastUpdated)?.TotalSeconds ?? 0) * timeFactor;
            LastUpdated = time;

            for (int i = 0; i < Balls.Count; ++i)
            {
                Balls[i].Gravitate(frameTimeSeconds, Gravity);
                Balls[i].Updated = Balls[i].ImpactBounding(frameTimeSeconds, Bounds);

                // If we've collided, Updated will now be true, which will make us skip this conditional.
                //   The '!' operator here means 'not', so this reads as; "if not ball updated"
                if (!Balls[i].Updated)
                {
                    // Only move when we didn't already move the ball to meet the edge of an impact.
                    Balls[i].Move(frameTimeSeconds);
                }

                Balls[i].ApplyFriction(frameTimeSeconds);
            }
        }
```

> Build and run now.  
> You should have the balls bouncing off the walls of the window!

> There are plenty of other ways to implement this idea of Updated. In general, I don't really like the vagueness of '`Updated`' nor the idea that the class using `Ball` has to know how to use it. If you'd like to improve on this design, you could:
>
> 1. Think of a better variable name.
> 2. Encapsulate this behaviour *within* the methods of `Ball`.
> 3. Make the property private.
>
> The final solution doesn't do this, but we should always leave code better than we found it. :)

## That's It for this Tutorial

Cool, you've done a lot!  
Give yourself a pat on the back, grab a drink, bask in the glory of being a *computer wizard*.

If anyone else did the javascript project they probably finished before you, but let's be honest, web programming is for noobs anyway. And they didn't implement *real-time physics*.

There's more you could do in this project. If you've gotten this far, you may want to implement collision between the balls. For that, I suggest you read [the tutorial that I used for the collision physics](https://www.gamasutra.com/view/feature/131424/pool_hall_lessons_fast_accurate_.php) which is excellent.

The final project code has an implementation of this, so feel free to go check that out if you're struggling.

Implement something hard, write some bugs into it, and learn about [debugging](https://code.visualstudio.com/docs/editor/debugging) with VS Code to find and solve these bugs.

*Thanks for the all the Fish.*
