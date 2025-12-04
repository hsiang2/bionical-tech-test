# User Management Module

This module provides functionality for managing users within a Bionical Solutions platform. It includes a user list view and an edit user form, as shown in the provided UI designs.

---

## User Stories

### User List View
- **US-01:** As an admin, I want to view a list of all users so that I can manage their details.
- **US-02:** As an admin, I want to see user details such as:
  - First Name
  - Last Name
  - Email
  - Gender
  - Date of Birth (DOB)
  - Age
  - Last Updated
  - Status (Active/Disabled)
- **US-03:** As an admin, I want to quickly identify the status of each user (Active or Disabled) using visual indicators.
- **US-04:** As an admin, I want to click an **Edit** icon next to a user to update their details.

### Edit User Form
- **US-05:** As an admin, I want to edit user details including:
  - First Name
  - Last Name
  - Email (with validation)
  - Gender (dropdown)
  - Date of Birth
  - Status (Active/Disabled)
- **US-06:** As an admin, I want to see validation errors if the email format is incorrect.
- **US-07:** As an admin, I want to see validation errors if a required field is not completed when a **Save** is attempted.
- **US-08:** As an admin, I want to retain my form changes if a save fails so I can make corrections and try again.
- **US-09:** As an admin, I want to **Cancel** changes and return to the user list without saving.
- **US-10:** As an admin, I want to **Save** changes and update the user record.
- **US-11:** As an admin, I want to see changes to the last updated date/time if a user has been saved.

### Bionical Solutions Platform
- **US-12** As an admin I want to be able to view all pages of the site on either a Desktop, Laptop, Table or Phone

---

## Technical Requirements

### Backend
- **Technology:** .NET 10
- **Architecture:** RESTful API
- **Endpoints:**
  - `GET /api/users` – Retrieve all users
  - `GET /api/users/{id}` – Retrieve a single user
  - `PUT /api/users/{id}` – Update user details
- **Data Validation:** Ensure proper email format and required fields.

### Frontend
- **Technology:** Vue.js 3 with Composition API (JavaScript or TypeScript)
- **Features:**
  - Responsive UI for user list and edit form.
  - Form validation for email and required fields.
- **Styling:** Use modern CSS or Tailwind for consistent design.

### Integration
- The Vue.js app will consume the .NET API endpoints.
- Use fetch for HTTP requests.
- Handle loading states and error messages gracefully.

## Future Enhancements
- Add pagination and search to the user list.
- Implement role-based access control.
- Add audit logs for user changes.

---

## What do you need to do?
- Add responsive styling to the site to match the included Figma designs
- Add a modal component that matches the designs and opens the right data based on the user selected
- Add an endpoint that uses `PUT` to receive the changes from your modal form and save them to the datastore
- Wire up the endpoint to the modal allowing a user to save or close.
- Add basic validation as per the user stories

---


## Instructions

- Designs for the solution can be seen in the included Figma file.
  - You can create a free login to Figma here (https://www.figma.com/):
- To run the api
  - Make sure .NET 10 is installed (https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
  - run `dotnet run --launch-profile "https"` from a terminal within the api folder
- To run the app
  - run `npm install` from a terminal within the app folder. Then run `npm run dev`
- Take no more than 6 hours on this test and submit your progress. It is OK if you don't do everything.
  - We want to see how you write code, solve problems and structure your solutions not how fast you can create as many features as possible.
- Submit the test via email with either a link to a public repo or an attached, zipped copy of the code.
  - Please be mindful of attachment sizes by excluding any binary or package files
- Document your project in a separate markdown file with anything you would like us to be aware of
  - What decisions did you make and why?
  - What changes would you suggest if you had more time?
  - What would you want to add before this was production ready?
- We are a collaborative team. If any of the above is on clear or you want to ask a question it will not be seen as a negative. Please reach out via email and I will respond as soon as possible.

