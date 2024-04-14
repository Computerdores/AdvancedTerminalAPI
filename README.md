# AdvancedTerminalAPI
[![](https://img.shields.io/badge/Computerdores-AdvancedTerminalAPI-brightgreen
)](https://thunderstore.io/c/lethal-company/p/Computerdores/AdvancedTerminalAPI/)

AdvancedTerminalAPI provides a simple way of adding new commands and terminals.
The primary motivation for the creation of this API was my personal dislike for the way Commands are implemented in the base game. For that reason this API is very different from the vanilla Implementation.
If you are looking for an API that is close to the way the vanilla commands are implemented, take a look at the [TerminalApi by NotAtomicBomb](https://github.com/NotAtomicBomb/TerminalApi/) which also inspired me to pick up this project. 

## Contributing
If you want to contribute just open a pull request and make sure your code follows the same style as the existing code base.

Also the [license](LICENSE) used by this project includes a [CLA](https://en.wikipedia.org/wiki/Contributor_License_Agreement).

## Building
Use `dotnet build -p:BuildThunderstorePackage=true` to build the Thunderstore package (Note: this requires `tcli` to be installed).

Use `dotnet build -p:BuildNugetPackage=true` to build the NuGet package.

## Credits
This project uses the [LethalCompanyTemplate by Distractic](https://github.com/Distractic/LethalCompanyTemplate) under the [MIT License](https://github.com/Distractic/LethalCompanyTemplate/blob/main/LICENSE).
