## Running API Tests with Postman

To run the API tests using Postman, follow these steps:

1. **Import the Postman Collection**:
   - Open Postman.
   - Click on the "Import" button in the top-left corner.
   - Select the `PostmanCollection.json` file from your file system to import it into Postman. This will create a new collection in Postman with all the API endpoints defined in the JSON file.

2. **Run the Login Request**:
   - In the newly created collection, find the request named "Login" (or similar).
   - Click on the "Send" button to execute the login request.
   - The response should include a token (usually in the response body).

3. **Set the JWT Token as a Variable**:
   - Copy the token from the login response.
   - In Postman, go to the "Environment" tab (usually found in the top-right corner).
   - Create a new environment or select an existing one.
   - Add a new variable named `jwt_token` and paste the copied token as its value.
   - Save the environment.

4. **Use the JWT Token in Subsequent Requests**:
   - Ensure that the `jwt_token` variable is used in the headers or wherever required in the subsequent requests within the collection.
   - You can reference the variable using `{{jwt_token}}`.

By following these steps, you will be able to run the API tests using the Postman collection and authenticate using the JWT token.