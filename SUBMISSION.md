## Overview

This submission implements a responsive user management interface based on the provided Figma designs. It includes a user list and an edit modal wired to the .NET API, with validation applied on both the frontend and backend.

---

## Key Technical Decisions

### Frontend
 - Implemented two layouts in the `user-list` component: a card-based list for mobile/tablet and a table for desktop. The switch is handled with conditional rendering and Tailwind responsive classes so each breakpoint matches the Figma design.

 - Used a shared `tableLabels` object so both layouts use the same wording without duplicating constants.

 - Added UI states for loading, error, and empty data so the list behaves predictably while data is being fetched.

 - The UI follows the Figma design across breakpoints. Since the design only covers the static states, a few small interaction details (such as hover opacity and a fixed background) were added to make the page feel more complete without changing the design direction.
 
### Backend
 -  Added server-side validation in `Update.cs` so the API checks the same required fields as the frontend and returns clear error messages when validation fails.

 - Used the C# record `with` expression to create an updated user object and refresh the `LastUpdated` field.

### Environment
 - On my macOS setup, running the API with the HTTPS launch profile caused a browser certificate error (“The certificate for this server is invalid”). During development, the frontend called the HTTP endpoint instead, which avoided the issue. No backend code was changed.

---

## Future Work

 - **TypeScript:** Migrate the Vue components and HTTP service to TypeScript to catch type errors earlier and make refactoring safer.

 - **Components:** Extract commonly used UI elements, such as input fields or repeated layout blocks, into shared components as the project grows.

 - **Enhancements:** Add search and basic client-side pagination so the user list remains easy to navigate with larger datasets.

---
## Production Readiness
 - **Database:** Replace the in-memory datastore with a real database so user updates can be stored permanently.

 - **Authentication and authorisation:** Add authentication and role-based access so that only authorised users can access or update user data.

 - **Testing:** Add automated tests for the API endpoints and key frontend interactions to ensure validation and update behaviour work as expected.
 
