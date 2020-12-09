# graphql.net

## Introduction

Makes using GraphQL in .NET as easy as applying an attribute {Api] to exposed properties.

Wraps the implementation of GraphQL by [Joe McBride](https://github.com/graphql-dotnet/graphql-dotnet). Using this implementation, the consumer needs to define many extra types like GraphTypes, GraphInputTypes, ... which is very time consuming and include repetitive code.

We eliminate this pain by using reflection to automatically generate those types.

## Usage

Follow these step:

1. Apply [Api] attribute to properties you would like to expose. You can optionally pass a false to make the property immutable.
2. Define your queries, mutations, and subscription.
3. To see an example how to do it see the project page

## Additional Services

We have also include some extra servcies that are frequently used by APIs: 

- Authentication using Firebase. Releated code is under Identity folder.
- Documenenation using Swagger. Releated code is under ApiDoc folder.

## Example

You can see an example under folder Example.
