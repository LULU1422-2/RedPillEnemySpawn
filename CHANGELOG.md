# Red Pill Changelog

## 0.3.0
### Added
- Obunga but outside

## 0.2.3
### Added
- Tidbits of information in config file
### Changed
- Removed misinformation from README (sort of)
- Slightly increased default player tracking range
- Intensified default proximity speed effect
### Fixed
- Patched DoAIInterval() instead of Update() when calculating movement speed (it did not need to be called every frame)

## 0.2.2
### Added
- Config entry for player detection radius
- More fun text in the README file

## 0.2.1
### Added
- Config entries
	- max spawn count
	- base movement speed
	- proximity effect (slow down) intensity
### Changed
- Smoother proximity-based speed effect

## 0.2.0
### Added
- Configuration! Now you can tweak the Red Pill's spawn rarity and disable the custom AI changes

## 0.1.0
### Added
- Added Red Pill to indoor enemy list at the start of a round (any moon)
### Changed
- Spawn rarity is now 2 (was 0)
- Max number of Red Pill enemies in facility is now 1 (was 0)
- Changed spawn probability curve to match the Bracken
- Changed the movement speed of Red Pill to speed up when far from its target and slow down when closing in
