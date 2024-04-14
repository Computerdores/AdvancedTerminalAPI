# Changelog

## [2.0.7](https://github.com/Computerdores/AdvancedTerminalAPI/compare/v2.0.6...v2.0.7) (2024-04-14)


### Bug Fixes

* **ci:** stop using `${{ github.repository_owner }}` in hopes of getting it working ([747e7f5](https://github.com/Computerdores/AdvancedTerminalAPI/commit/747e7f515d743b8aa32d87b0ee5ea4168e417e46))

## [2.0.6](https://github.com/Computerdores/AdvancedTerminalAPI/compare/v2.0.5...v2.0.6) (2024-04-14)


### Bug Fixes

* another attempt to fix the ci ([76d8da8](https://github.com/Computerdores/AdvancedTerminalAPI/commit/76d8da8779799971a8979611cc6f45577c454e4f))

## [2.0.5](https://github.com/Computerdores/AdvancedTerminalAPI/compare/v2.0.4...v2.0.5) (2024-04-14)


### Bug Fixes

* ci/cd pipeline should work now ([fa8d3ec](https://github.com/Computerdores/AdvancedTerminalAPI/commit/fa8d3ec583315289d292436d9f3202171d41739e))

## [2.0.4](https://github.com/Computerdores/AdvancedTerminalAPI/compare/v2.0.3...v2.0.4) (2024-04-14)


### Bug Fixes

* remove unnecessary C# project ([a429909](https://github.com/Computerdores/AdvancedTerminalAPI/commit/a429909d355cf99a6b427a1d824910ff7e93b2fe))

## [2.0.3](https://github.com/Computerdores/AdvancedTerminalAPI/compare/v2.0.2...v2.0.3) (2024-04-14)


### Bug Fixes

* correct category name in thunderstore.toml to fix thunderstore upload ([635096a](https://github.com/Computerdores/AdvancedTerminalAPI/commit/635096af80cfbc226e8f6c795b3eca947984dc13))

## [2.0.2](https://github.com/Computerdores/AdvancedTerminalAPI/compare/v2.0.1...v2.0.2) (2024-04-14)


### Bug Fixes

* add category "client-side" for Thunderstore upload ([d545879](https://github.com/Computerdores/AdvancedTerminalAPI/commit/d54587960c53ec142e3ef566b1caaa1634dff379))
* move code from namespace 'Computerdores' to 'Computerdores.AdvancedTerminalAPI' ([9c8d88e](https://github.com/Computerdores/AdvancedTerminalAPI/commit/9c8d88e4d191d457d10b824883147f2edcbe4a5d))

## [2.0.1](https://github.com/Computerdores/AdvancedTerminalAPI/compare/v2.0.0...v2.0.1) (2024-04-12)


### Bug Fixes

* update icon ([538709c](https://github.com/Computerdores/AdvancedTerminalAPI/commit/538709c91d14578e63d0f2522220d9abaedac832))

## [2.0.0](https://github.com/Computerdores/AdvancedTerminalAPI/compare/v1.0.0...v2.0.0) (2024-04-12)


### ⚠ BREAKING CHANGES

* Overhaul API for loading Nodes by adding a Wrapper Class

### Features

* add IDescribable Interface and implement it in Vanillin Commands ([aadfb1a](https://github.com/Computerdores/AdvancedTerminalAPI/commit/aadfb1a6fd6518a30b78983328122ea7aff953bc))
* ITerminals now need to implement GetCommands ([dafadf4](https://github.com/Computerdores/AdvancedTerminalAPI/commit/dafadf4254422af7ef7c5ceef9bbcc323809b57a))
* **OtherCommand:** rework to show commands that use the IDescribable Interface ([c9a0826](https://github.com/Computerdores/AdvancedTerminalAPI/commit/c9a08263ec85db76ad43f3aaa7d17330737fe5c9))
* Overhaul API for loading Nodes by adding a Wrapper Class ([27fedeb](https://github.com/Computerdores/AdvancedTerminalAPI/commit/27fedeba4e92d070035e25b5c0fe4246bb989bd1))


### Bug Fixes

* **AdvancedTerminalAPI:** Fix NullReferenceException ([67747e7](https://github.com/Computerdores/AdvancedTerminalAPI/commit/67747e7bbdc637c62ecfe5ba4b067b1c0f2ff231))
* **InputFieldDriver:** remove unnecessary types ([a60cd58](https://github.com/Computerdores/AdvancedTerminalAPI/commit/a60cd58d8f90666fc7c97b05652e0cb3b507405e))
* remove 'test' command ([2ddbee9](https://github.com/Computerdores/AdvancedTerminalAPI/commit/2ddbee96834a7b72cb389b6fee739e1fe67eb16d))
* **VanillinTerminal:** GetCommands now puts built-ins before other commands ([bba6dd1](https://github.com/Computerdores/AdvancedTerminalAPI/commit/bba6dd1a6ec5ea6c32067b562d1877c065c719a0))

## 1.0.0 (2024-04-10)


### ⚠ BREAKING CHANGES

* Change naming in IPredictable API to clearly convey that not just arguments but also Input should be predicted
* **InputFieldDriver:** Change DisplayText to offer clearing the Screen instead of the input
* **InputFieldDriver:** add OnInputChange Event
* Move ICommand.PredictArguments to a seperate interface (IPredictable)
* Changes to API and Vanillin Shell to allow for commands to accept input

### Features

* add a generic wrapper for vanilla load methods ([01ef577](https://github.com/Computerdores/AdvancedTerminalAPI/commit/01ef577e3dec0df99bff554f449389e801937d89))
* Add help command ([baf4e8b](https://github.com/Computerdores/AdvancedTerminalAPI/commit/baf4e8b9509331cf1c4e8839118234c4fea832e7))
* Add IAliasable for e.g. commands which can be shortened by leaving out the name ([c93dcf1](https://github.com/Computerdores/AdvancedTerminalAPI/commit/c93dcf1dd758014fd4ff5c8e27879621d039598e))
* **BuyCommand:** now implements IAliasable ([78f078c](https://github.com/Computerdores/AdvancedTerminalAPI/commit/78f078c1e0e247d94c5a974aeced94532b4ace56))
* Changes to API and Vanillin Shell to allow for commands to accept input ([dbe84a0](https://github.com/Computerdores/AdvancedTerminalAPI/commit/dbe84a06c1f8a170c603853945ab4d9f85d0ff21))
* Implement 'bestiary' command ([9597e8c](https://github.com/Computerdores/AdvancedTerminalAPI/commit/9597e8cbcc73449fbfeab520d922d66f9f4be82f))
* Implement 'buy' command and accompanying commands for individual items ([d73af78](https://github.com/Computerdores/AdvancedTerminalAPI/commit/d73af78b0749a0f63a34af8dfac6e8a840839a4d))
* Implement 'decor' & 'upgrades' commands ([d0732e5](https://github.com/Computerdores/AdvancedTerminalAPI/commit/d0732e5dc9b957dded2aaf29dd2538977ea03570))
* Implement 'eject' command ([67ab2ac](https://github.com/Computerdores/AdvancedTerminalAPI/commit/67ab2aca91e31356c3b838a342f75050ca8c0aaa))
* Implement 'flash' command ([eaad100](https://github.com/Computerdores/AdvancedTerminalAPI/commit/eaad1004b4b044a835a921d9fdd8486f800e448b))
* Implement 'info' command ([1a0db30](https://github.com/Computerdores/AdvancedTerminalAPI/commit/1a0db30137b6927ad32f6d306ff945f4e17d3711))
* Implement 'moons' command ([7a1966f](https://github.com/Computerdores/AdvancedTerminalAPI/commit/7a1966fb891f14ca2da494978bbf30421071a94d))
* Implement 'other' command ([76d6953](https://github.com/Computerdores/AdvancedTerminalAPI/commit/76d695331578bb3c0f5eb468bd7a064137ea25bd))
* Implement 'ping' command ([cc01794](https://github.com/Computerdores/AdvancedTerminalAPI/commit/cc01794cc02442d8adbe4af2a1e0bbe9db5225d0))
* Implement 'route' command and accompanying commands for individual moons ([c5e361e](https://github.com/Computerdores/AdvancedTerminalAPI/commit/c5e361ec87dd47feb595781f5fb530edceabc187))
* Implement 'scan' command ([3a0180d](https://github.com/Computerdores/AdvancedTerminalAPI/commit/3a0180d5aa083b556120df76d3a18fced38bb23b))
* Implement 'sigurd' command ([d814913](https://github.com/Computerdores/AdvancedTerminalAPI/commit/d81491383fe7836ec200c152fce2ad51dd1283f1))
* Implement 'storage' command ([bb73821](https://github.com/Computerdores/AdvancedTerminalAPI/commit/bb73821bcc760aff5eb6bb4fcae18b20af620ee1))
* Implement 'store' command ([90ab1a9](https://github.com/Computerdores/AdvancedTerminalAPI/commit/90ab1a981f7aa23424cdc77caeaa4ac126ff0c66))
* Implement 'transmit' command ([1676cc9](https://github.com/Computerdores/AdvancedTerminalAPI/commit/1676cc95f58c0f7a4bec54c1c3246aadaaed3d97))
* Implement command for terminal accessible objects (doors, turrets, etc.) ([7b2df24](https://github.com/Computerdores/AdvancedTerminalAPI/commit/7b2df24f659cc99a89ee5102252f11617d97a10d))
* implement SpecialNodeCommand and replace WelcomeCommand and HelpCommand with it ([52362d5](https://github.com/Computerdores/AdvancedTerminalAPI/commit/52362d59372d6ee69b022a48db5dea1791717cc3))
* implement switch command ([83aeb1c](https://github.com/Computerdores/AdvancedTerminalAPI/commit/83aeb1c47da83cbeb75b030e61d8f2aca7fb6003))
* **InfoCommand:** now implements IAliasable ([930e9af](https://github.com/Computerdores/AdvancedTerminalAPI/commit/930e9af69780103364a7ef0f9df30422f77dedec))
* **InputFieldDriver:** add OnInputChange Event ([cd4955b](https://github.com/Computerdores/AdvancedTerminalAPI/commit/cd4955b3a73ce67757180ba20e934e1ee5f469c9))
* **InputFieldDriver:** Change DisplayText to offer clearing the Screen instead of the input ([7547e90](https://github.com/Computerdores/AdvancedTerminalAPI/commit/7547e905ece9b18569fe95374c1a5cb5638f0a22))
* **InputFieldDriver:** expose MaxInputLength on the public API ([d8ceb51](https://github.com/Computerdores/AdvancedTerminalAPI/commit/d8ceb511491a6d04c6a52bcdefe52239cea565d4))
* Major API rework for better replacing of the Terminal and adding of commands ([0d04e5a](https://github.com/Computerdores/AdvancedTerminalAPI/commit/0d04e5abb42cfcac650da576101272ac9e51860a))
* Plugin Builds and Shows awake message ([165f1bb](https://github.com/Computerdores/AdvancedTerminalAPI/commit/165f1bbd8f850e39c19b3085bbe31b8359853d3a))
* **RouteMoonCommand:** now implements IAliasable ([ad480da](https://github.com/Computerdores/AdvancedTerminalAPI/commit/ad480da93176c0ec85c26de07ec41ab8abdd5909))
* sigurd log entries are now accessible without 'view' ([17d6c29](https://github.com/Computerdores/AdvancedTerminalAPI/commit/17d6c29add8f17b352897b12a8bab7fe50335229))
* **TerminalPatch:** add LoadNewNode and LoadNewNodeIfAffordable ([a0de98b](https://github.com/Computerdores/AdvancedTerminalAPI/commit/a0de98b323eb5651066404e58e8b0b2f0e45e6e0))
* **TerminalPath:** add pre and post events for awake, start and update ([74accae](https://github.com/Computerdores/AdvancedTerminalAPI/commit/74accae33a1ea01ebb0989572103f32034d0f2fa))
* **VanillinTerminal:** Publicly expose IEnumerable of all Builtin Commands ([72a754e](https://github.com/Computerdores/AdvancedTerminalAPI/commit/72a754e9440776c5a6ac1f9a573e3b306f8062c2))
* view command now working ([b64e9a5](https://github.com/Computerdores/AdvancedTerminalAPI/commit/b64e9a5b324513c4e423d66d79916e60c932fda8))
* **ViewCommand:** now implements IAliasable ([f13344a](https://github.com/Computerdores/AdvancedTerminalAPI/commit/f13344a75e26cd207b9d842e9b747494b8b41309))


### Bug Fixes

* accidental bypass of Unity.Object Lifetime check ([38e7de2](https://github.com/Computerdores/AdvancedTerminalAPI/commit/38e7de228631c7762ff1cea29102e09d93183bba))
* Adapt ICommand.Execute Signature to better suit future needs ([018be78](https://github.com/Computerdores/AdvancedTerminalAPI/commit/018be78a641eb2bee6e7c4222b383f122bfc7e96))
* add debug log for terminal nodes and keywords ([c865e37](https://github.com/Computerdores/AdvancedTerminalAPI/commit/c865e37fdaee50d91c20a9b088eaacc38553e4d3))
* add version.txt ([d73335b](https://github.com/Computerdores/AdvancedTerminalAPI/commit/d73335bf2c26609675e20ae56fe7c01044363460))
* **BuyCommand:** you can now actually buy all items ([c78cbf6](https://github.com/Computerdores/AdvancedTerminalAPI/commit/c78cbf695aeb1c4f74319ef39daedec907c7075c))
* **BuyUnlockableCommand:** now clones the correctly ([a5b7249](https://github.com/Computerdores/AdvancedTerminalAPI/commit/a5b7249c2fe1ccda547321e7f35b6b3dcd7d3132))
* Change naming in IPredictable API to clearly convey that not just arguments but also Input should be predicted ([ab52e49](https://github.com/Computerdores/AdvancedTerminalAPI/commit/ab52e4914f18585ab3f9f7cf3052d4af1a680bf5))
* change SwitchCommand implementation to be closer to the vanilla one ([8da9f5d](https://github.com/Computerdores/AdvancedTerminalAPI/commit/8da9f5d26809c0e489ea7ee2ad867118e43ab7bd))
* **CommandResult:** add constants (technically static readonlys) for a generic error and for ignoring the given input ([2cacc35](https://github.com/Computerdores/AdvancedTerminalAPI/commit/2cacc35145a780a608767071f7e2a966c88c6e83))
* **CommandResult:** add default values for clearScreen and success ([d3dc6a0](https://github.com/Computerdores/AdvancedTerminalAPI/commit/d3dc6a07cf3825445b793f4466548eaea86c122f))
* **CommandResult:** Correctly apply default values ([f28499a](https://github.com/Computerdores/AdvancedTerminalAPI/commit/f28499a46ea1084b2a7ac74e3ed01e84e6e71da8))
* **EjectCommand:** adhere closer to the vanilla command ([5b9d3fb](https://github.com/Computerdores/AdvancedTerminalAPI/commit/5b9d3fbf18c1ed81711bbd85880982fa7a84a3c9))
* **EjectCommand:** Clone now works correctly ([39138ea](https://github.com/Computerdores/AdvancedTerminalAPI/commit/39138ea6bd9b050e19f44ec0f6301b56d0ad11c5))
* **HelpCommand:** use the vanilla string formatting ([35bb444](https://github.com/Computerdores/AdvancedTerminalAPI/commit/35bb444562b1be765244cb70ba13c651934de4ed))
* **InfoCommand:** you can now leave out the info keyword ([f44d5b3](https://github.com/Computerdores/AdvancedTerminalAPI/commit/f44d5b3a302b649d3bc693306310c1f110f97193))
* **InfoThingCommand:** Creature file now get loaded correctly ([cc51e76](https://github.com/Computerdores/AdvancedTerminalAPI/commit/cc51e76ec9747ca843f36b0a2544d940bcd53366))
* **InputFieldDriver:** DisplayText now ACTUALLY follows intended behaviour when text is null ([e6003fb](https://github.com/Computerdores/AdvancedTerminalAPI/commit/e6003fb6b841a521ca9491ed2ec2728894503d52))
* **InputFieldDriver:** DisplayText now follows intended behaviour when text is null ([431c4ef](https://github.com/Computerdores/AdvancedTerminalAPI/commit/431c4eff7e78d33d75254138455b5c5b56a8f79f))
* ITerminal now exposes the InputFieldDriver Instance ([9151779](https://github.com/Computerdores/AdvancedTerminalAPI/commit/9151779e9b01bdaa28ad926aedfcc6dd95edb026))
* **ITerminal:** remove unused methods ([a8eca58](https://github.com/Computerdores/AdvancedTerminalAPI/commit/a8eca5866750923bb98688ba1583b2df96256ce7))
* Move ASimpleCommand to a more appropriate namespace ([1d8b342](https://github.com/Computerdores/AdvancedTerminalAPI/commit/1d8b34291cf0a82368a89312fe2c12d709c229ff))
* move default commands and terminal to a common namespace ([77d47cc](https://github.com/Computerdores/AdvancedTerminalAPI/commit/77d47ccdca55c496da330f5510c5578cf5ca30ac))
* Move ICommand.PredictArguments to a seperate interface (IPredictable) ([f14575e](https://github.com/Computerdores/AdvancedTerminalAPI/commit/f14575e35aae29d61339e9e345d189a0c6878bcd))
* move static methods to a common Util class ([cb6030c](https://github.com/Computerdores/AdvancedTerminalAPI/commit/cb6030cf655dba8cdcc76c74403e6419b3f95a61))
* prevent Vanillin Commands from being copied to other terminals when it is replaced ([a4dd8c8](https://github.com/Computerdores/AdvancedTerminalAPI/commit/a4dd8c87954d66e99d38f9d080aac9de833f67d3))
* remove unused private method ([25d2af3](https://github.com/Computerdores/AdvancedTerminalAPI/commit/25d2af3faa33209ca055049ff15158b83a17fee9))
* rename ASimpleCommand to SimpleCommand in order to adhere to dotnet naming conventions ([7b2adb0](https://github.com/Computerdores/AdvancedTerminalAPI/commit/7b2adb0b7a190de8684ee770d3b47cd719152c60))
* shift responsibility for enumerating all AccessibleObjectCommands to AccessibleObjectCommand ([7eb42f3](https://github.com/Computerdores/AdvancedTerminalAPI/commit/7eb42f36ce2bcf4f45ed00cfd7661636aa14758b))
* shift responsibility for enumerating all SimpleCommands to SimpleCommand ([a0fc8bf](https://github.com/Computerdores/AdvancedTerminalAPI/commit/a0fc8bfd78d8a65559e279bb4645f9941038894b))
* SwitchCommand now pulls special string from the game in case it changes ([2d17e62](https://github.com/Computerdores/AdvancedTerminalAPI/commit/2d17e62ad963d0a1b823f0648781c028fbf2bd4f))
* **TerminalPatch:** use nameof instead of hardcoded strings ([3530537](https://github.com/Computerdores/AdvancedTerminalAPI/commit/3530537b17d7a1f4c442b41f94eb0bc4b2641a69))
* update InputFieldDriver API to somewhat future proof it ([f11f64d](https://github.com/Computerdores/AdvancedTerminalAPI/commit/f11f64d526343cf62ae1bbd20e9c1ff9607e3939))
* **Util:** FindTerminalOption now uses the correct specificity ([efbf5b9](https://github.com/Computerdores/AdvancedTerminalAPI/commit/efbf5b9b5e0a1a4a5066fa282b27f1739c1abc26))
* **Util:** move Util to more appropriate namespace ([d2fae97](https://github.com/Computerdores/AdvancedTerminalAPI/commit/d2fae974e3101d1e702816f943ceab7a013e6b7d))
* VanillinTerminal now pulls special strings from the game in case they change ([78a3388](https://github.com/Computerdores/AdvancedTerminalAPI/commit/78a338881ecc383168ce6a01a79b9db1ef50d4f7))
* VanillinTerminal now pulls the list of terminal accessible objects from the game ([c92c121](https://github.com/Computerdores/AdvancedTerminalAPI/commit/c92c121948ab258ae17dd59c9fcc07ddb88a08f6))
* **VanillinTerminal:** allow for custom error messages ([1c048c0](https://github.com/Computerdores/AdvancedTerminalAPI/commit/1c048c0c220c56d13b5a28c6071502b80caa68f2))
* **VanillinTerminal:** Execution of a command will now be terminated if execution fails instead of forcing the player to reopen the terminal. ([24dc0f5](https://github.com/Computerdores/AdvancedTerminalAPI/commit/24dc0f5801c39cffdb403feba70319a5c81ebd4e))
* **VanillinTerminal:** fix a bug where a command would not be found because more than 1 command matched the input ([e053c0c](https://github.com/Computerdores/AdvancedTerminalAPI/commit/e053c0ce2d1a14469127e8198b65ae4743aeb130))
* **VanillinTerminal:** prevent empty input from triggering a command not found message ([ba50015](https://github.com/Computerdores/AdvancedTerminalAPI/commit/ba500150e05066274b465c8b795b67858adf3736))
* **VanillinTerminal:** respect clearScreen even when success == false ([6437fbc](https://github.com/Computerdores/AdvancedTerminalAPI/commit/6437fbcba474eed85a9b8ba4071473233583b172))
* **VanillinTerminal:** use vanilla string matching ([4137a3b](https://github.com/Computerdores/AdvancedTerminalAPI/commit/4137a3bd929d3561060e494435afee8e95c900b5))
* **ViewCommand:** ignore case ([bd2484c](https://github.com/Computerdores/AdvancedTerminalAPI/commit/bd2484ce4d654f95bedd6e2cfa457f68a0884f43))
* **ViewThingCommand:** Log Files now get loaded correctly ([cea2f8e](https://github.com/Computerdores/AdvancedTerminalAPI/commit/cea2f8ea8a7d979894519941d1263f9d6ac798cc))
* WelcomeCommand now pulls special string from the game in case it changes ([2917f8f](https://github.com/Computerdores/AdvancedTerminalAPI/commit/2917f8f0e4c5a7348875702d2236a5ea7cdf9fe2))
* **WelcomeCommand:** use the vanilla string formatting ([5d502dc](https://github.com/Computerdores/AdvancedTerminalAPI/commit/5d502dca00ee0246219b92354cdf52ddc18ebcdc))
