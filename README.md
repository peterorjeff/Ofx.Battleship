# OFX Battleship State Tracker
This repo is a coding test for [OFX](https://www.ofx.com), with an implementation of a Battleship state tracker API.

## The Task
The task is to implement a Battleship state tracking API for a single player that must support the following logic:

- Create a board
- Add a battleship to the board
- Take an “attack” at a given position, and report back whether the attack resulted in a hit or a miss.

The API should not support the entire game, just the state tracker. No graphical interface or persistence layer is required.

## Overview
I have attempted to follow clean archtitecture with a CQRS implementation.
The implementation is split into the following layers:

- Core
  - [Domain](./Src/Domain/README.md)
  - [Application](./Src/Application/README.md)
- Infrstructure
  - [Persistence](./Src/Persistence/README.md)
- Presentation
  - WebAPI

Unit and Integration testing has been added for:

- Commands
- Command Validation
- Controllers

## Deployment
The application has been deployed to AWS and is available here: http://battleship-alb-1867546237.ap-southeast-2.elb.amazonaws.com/api  
Swagger / OpenAPI docs available here: http://battleship-alb-1867546237.ap-southeast-2.elb.amazonaws.com/index.html

## References
I made use of several libraries to aid clean architecture:

- Mediatr: https://github.com/jbogard/MediatR
- AutoMapper: https://automapper.org/
- Fluent Validation https://fluentvalidation.net/
- Fluent Assertions: https://fluentassertions.com/introduction
