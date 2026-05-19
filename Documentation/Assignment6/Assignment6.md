# Assignment 6 - Dispatcher Status

## Completed

* [x] Command dispatcher interface
* [x] Command dispatcher implementation
* [x] Interaction tests for dispatcher delegation
* [x] Failure test for missing command handler
* [x] Dispatcher decorator
* [x] Decorator tests

## Implemented Files

* `ViaPadelClub-Kim-DCA1.Core.Application/Dispatching/ICommandDispatcher.cs`
* `ViaPadelClub-Kim-DCA1.Core.Application/Dispatching/CommandDispatcher.cs`
* `ViaPadelClub-Kim-DCA1.Core.Application/Dispatching/NullCommandDispatcherDecorator.cs`
* `UnitTests/Application/Dispatching/CommandDispatcherTests.cs`
* `UnitTests/Application/Dispatching/NullCommandDispatcherDecoratorTests.cs`

## Optional Work

* [ ] Add automatic registration of command handlers with `IServiceCollection`
* [ ] Add Web API integration when the Web API project is created

## Notes

The dispatcher delegates commands to registered `ICommandHandler<TCommand>` implementations.
The decorator rejects null commands before delegating to the inner dispatcher.
