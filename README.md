# Mithril

## Introduction
Welcome to Mithril, a research project and library aimed at simplifying application development through various modularization, CQRS-style API, and plugin architecture concepts. Mithril is primarily designed to expedite the development process of internal-facing applications, focusing on simplicity and speed of development.

## Purpose
Mithril serves as a sandbox for experimenting with development concepts and methodologies. Its primary goal is to test and validate ideas to determine their efficacy in simplifying and accelerating the development workflow. As such, the library is continuously evolving as concepts are explored, refined, and integrated. That means that while the code is useable, major changes can and will happen. As useful concepts are discovered or useful code is written, libraries will be broken off for reuse and placed on NuGet and/or NPM. For example:

* [Gestalt](https://github.com/JaCraig/Gestalt) is a C# library designed to facilitate the development of modular applications. It allows developers to create reusable modules that can be easily integrated into various applications, enhancing code reusability and maintainability.
* [@jacraig/request](https://github.com/JaCraig/request) a lightweight library designed to simplify HTTP requests using the native fetch API while adding functionality such as caching, retry logic, and timeouts.
* [@jacraig/woodchuck](https://github.com/JaCraig/Woodchuck) a logging library for TypeScript/JavaScript based on the concepts of structured event data found in Serilog.

As well as improvements to some of my existing libraries.

## Features Currently Exploring
* Modularization: Mithril promotes a modular approach to application development, allowing components to be developed, tested, and maintained independently, facilitating scalability and code organization.
* CQRS-style API: Mithril adopts the Command Query Responsibility Segregation (CQRS) pattern, separating read and write operations to improve performance, scalability, and maintainability.
* Plugin Architecture: Mithril facilitates extensibility through a plugin architecture, enabling developers to easily integrate and extend functionality as needed, without tightly coupling components.
* Testing: Automatic test generation, property testing, fuzzing, etc. to simplify fatal bug discovery.
* Minimal APIs: Possible use cases and pitfalls.
* Automatic generation of admin tooling
