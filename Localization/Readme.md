Basic Localization system.

A little project to see how much afford it would be to implement a "GNU gettext" compatible system in Unity.

#### TODO
- [ ] write or update a GNU gettext compatible [.po file](https://www.gnu.org/software/gettext/manual/html_node/PO-Files.html) after collecting all translatable text
- [ ] GNU gettext [.mo file](https://www.gnu.org/software/gettext/manual/html_node/MO-Files.html) parser in Unity or incorporate [existing c# library](https://www.gnu.org/software/gettext/manual/html_node/C_0023.html)
- [ ] add support for multi-line text
- [ ] add support for other grammatical numbers
- [ ] more reliable way to display the translated text in edit mode

#### Usage
Add the prefab LocalizationManager to the scene.
The files containing the translations should be located under "StreamingAssets/Locales/".

For a static text, i.e. a button with the text "Abort"
- add the LocalizedText script to a GameObject with Text component
  - textId: The identifier for the desired localized text (can be a generic string like "btn_abort" or the default language option like "Abort")

For a variable text, i.e. texts with changing numbers or editable names 
- add the LocalizedText script to a GameObject with Text component
  - textId: The identifier for the desired localized text, i.e "You have {player.coins} coin."
  - textIdPlural: Optional identifier if the text has a plural version, i.e. "You have {player.coins} coins."
  - pluralVariable: If textIdPlural is set the plural variable must be specified
- add one or more implementations of AbstractVariableTextContent to the GameObject (see Scripts/Example/BombTimeProvider.cs together with the Bomb prefab)
  - key: The variable Identifier referenced in the localized text, i.e. "player.coins"
- there can be more than one variable per text, but only one of those can represent a variable grammatical number, i.e. "{character.name} gained {difference} health points."

Collect the textIds from all Prefabs with one or more LocalizedText component via the Editor menu item.
