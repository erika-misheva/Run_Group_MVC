# Running Club Web Application

This is an ASP.NET MVC web application designed for runners to connect with running clubs, find fellow runners, discover races, and manage their profiles.

## Features

1. **Home Page:**
   - Displays running clubs near the user's location (if available).
   - Provides an overview of the running community.

2. **Find Runners Page:**
   - Available for logged-in users.
   - Users can view profiles of other runners.
   - Clicking on "View" displays more detailed information about the selected runner.

3. **Find Clubs Page:**
   - Shows existing running clubs.
   - Displays club members and the creator.
   - Logged-in users can join clubs.
   - Users can create their own running clubs.

4. **Find Races Page:**
   - Lists all upcoming races.
   - Clicking on "View" shows detailed information about the race, including contact info for joining.

5. **Login and Register Pages:**
   - Allows users to log in or register a new account.

6. **Dashboard Page (After Login/Register):**
   - Users can create races or clubs.
   - Users can edit their profiles.
   - Users can delete their races or clubs if already created.

7. **Admin Panel:**
   - Admin users have the ability to edit or delete any race or club in the system.

## Styling

- The application utilizes Bootstrap classes for styling, ensuring a responsive and visually appealing design.
  
## Getting Started

Go into directory where you plan on keeping project and run.
git clone https://github.com/erika-misheva/Run_Group_MVC.git

Create a local database.

Add connection string to app settings.json. It will look something like this:
Data Source=DESKTOP-EI2TOGP\\SQLEXPRESS;Initial Catalog=RunGroops;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

Register for a Cloudinary Account (%100 free) and add Cloudname, ApiKey, and Api secret to appsettings.json.
