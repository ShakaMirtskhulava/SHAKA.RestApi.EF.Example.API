# Idempotency Testing Guide

This folder contains HTTP request files for testing the idempotency features of the API. These files are designed to be used with the VS Code REST Client extension or similar tools that support .http files.

## Test Files

1. **basic-idempotency-test.http**: Basic idempotency tests showing how repeated requests with the same idempotency key return the same response.

2. **concurrent-requests-test.http**: Tests how the system handles concurrent requests with the same idempotency key.

3. **error-handling-test.http**: Tests how idempotency works with error responses.

4. **expiration-test.http**: Demonstrates idempotency key expiration behavior (requires manual execution with appropriate timing).

5. **http-methods-test.http**: Tests idempotency with different HTTP methods like POST and PUT.

## How to Run the Tests

1. Install the [REST Client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) for VS Code.

2. Make sure your API is running locally at the URL specified in the @baseUrl variable in each file (default is https://localhost:7251).

3. Open one of the .http files and click "Send Request" on each request individually, following the sequence and instructions in the comments.

## Expected Behavior

- When making multiple identical requests with the same idempotency key, only the first should actually perform the operation. Subsequent requests should return the cached response.

- Using a new idempotency key should result in a new operation being performed.

- Error responses (non-2xx) should not be cached for idempotency.

- After the expiration time has passed, the idempotency key should no longer return cached results.

## Troubleshooting

If the idempotency feature doesn't seem to be working:

1. Check that the middleware is correctly registered in Program.cs:
   ```csharp
   app.UseIdempotency();
   ```

2. Verify that your DbContext has the IdempotentRequest entity configured correctly.

3. Make sure you're using the correct header name (X-Idempotency-Key by default).

4. Check the server logs for any errors related to the idempotency middleware.

5. Confirm that your database migrations have been applied successfully.
