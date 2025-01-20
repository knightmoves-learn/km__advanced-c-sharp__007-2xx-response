# 007 2XX Response

## Lecture

[![# 007 2XX Response](https://img.youtube.com/vi/N5mlX1WL0mg/0.jpg)](https://www.youtube.com/watch?v=N5mlX1WL0mg)

## Instructions

In this assignment you will continue working on our HomeEnergyApi's Controller. Similarly to the lecture, you will modify our POST method to return the `201: Created` HTTP response.

In `HomeEnergyApi/Controllers/HomesController.cs`...

- Modify the POST method `CreateHome()`
  - On success, this method should return the HTTP response `201: Created`
  - This method should respond with the home being added.
  - This method should contain in its response headers the `Location` the home being added is created at.
    - Hint: Unlike the code examples in the lecture, you cannot assume homesList is sorted by `id`. The `Home` with an `id` of 2, may not necessarily be the `Home` at `homesList[2]`
  - This method should still add the home from the body of the POST request to the static list `homesList`
- Any other existing methods or properties on `HomesController.cs` should NOT be changed.
- All methods should use the base route `/Homes`.

Additional Information:

- You should ONLY make code changes in `HomeEnergyApi/Controllers/HomesController.cs` to complete this assignment.

## Building toward CSTA Standards:

- Explain how abstractions hide the underlying implementation details of computing systems embedded in everyday objects (3A-CS-01) https://www.csteachers.org/page/standards
- Demonstrate code reuse by creating programming solutions using libraries and APIs (3B-AP-16) https://www.csteachers.org/page/standards

## Resources

- https://en.wikipedia.org/wiki/List_of_HTTP_status_codes
- https://en.wikipedia.org/wiki/Request%E2%80%93response

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
