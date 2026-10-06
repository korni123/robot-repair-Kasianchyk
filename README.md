Навчальний проєкт з дисципліни «Геймдизайн та програмування ігрових за-
стосунків». РФКІТ, група ІПЗ-3/2, 2026/27.

## Проєкт
Adventure Game: Robot Repair (Unity Technologies), Unity 6.3, URP.
## Як запустити

Відкрити теку проєкту через Unity Hub, редактор 6.3, відкрити сцену рівня, натиснути Play.

## Що зроблено
• Заняття 3: проєкт запущено, перемкнуто набір артів
• Юніт 1: сцена MainScene, тайлмап із Rule Tile і палітрою Game Palette, робот ходить стрілками, WASD і стіком геймпада з Time.deltaTime
• Юніт 1, More things to try: план рівня ([Docs/level-plan.png](Docs/level-plan.png)), стік геймпада в MoveAction

## Щоденник розробника

### 22.09.2026 — Юніт 1: персонаж і рух

**Що зроблено:** створив сцену MainScene, додав Grid і Tilemap, зробив два Rule Tile (FirstTile і SecondTile) і палітру Game Palette, поставив Tile1 і Tile2 Pixels Per Unit = 64, а Tilemap — Order in Layer = −10. Додав PlayerCharacter і написав PlayerController: ввід через Input System (InputAction MoveAction, композит Up/Down/Left/Right), рух через transform.position. 06.10 доробив пропущене: композит на стрілки поруч із WASD, множник Time.deltaTime і швидкість 3.0f, а з More things to try — план рівня картинкою й стік геймпада (Add Binding → Gamepad → Left Stick).

**Що не вийшло або забрало найбільше часу:** прив'язки в MoveAction — спершу зробив лише WASD і забув стрілки. Unity один раз вилетів і відновив сцену з резервної копії.

**Що зрозумів про Update і Time.deltaTime:** Update викликається щокадру, тому 0.01f за кадр на 60 і на 300 FPS — це зовсім різна швидкість. Time.deltaTime — час від попереднього кадру, тому 3.0f * Time.deltaTime означає три одиниці за секунду на будь-якому комп'ютері. Без нього на повільному комп'ютері персонаж повзе. Композит сам нормалізує вектор, тому по діагоналі (0.71, 0.71), а не (1, 1).

**Що робитиму далі:** справжній рівень, декорації й фізика, щоб робот не ходив крізь перешкоди.
