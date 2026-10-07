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
• Юніт 2: рівень із тайлсетів, декорації й зона шкоди префабами, сортування за Y, Rigidbody 2D і колайдери, рух через MovePosition у FixedUpdate, колайдер тайлмапа
• Юніт 2, More things to try: тайлмап переднього плану (арка поверх робота), 9-slicing будівлі
• Юніт 3: здоров'я робота (maxHealth, ChangeHealth, невразливість), аптечки HealthCollectible, зона шкоди DamageZone з тригером
• Юніт 3, More things to try: ultra damage zone (варіант префаба, damage = 2), slow healing zone

## Щоденник розробника

### 22.09.2026 — Юніт 1: персонаж і рух

**Що зроблено:** створив сцену MainScene, додав Grid і Tilemap, зробив два Rule Tile (FirstTile і SecondTile) і палітру Game Palette, поставив Tile1 і Tile2 Pixels Per Unit = 64, а Tilemap — Order in Layer = −10. Додав PlayerCharacter і написав PlayerController: ввід через Input System (InputAction MoveAction, композит Up/Down/Left/Right), рух через transform.position. 06.10 доробив пропущене: композит на стрілки поруч із WASD, множник Time.deltaTime і швидкість 3.0f, а з More things to try — план рівня картинкою й стік геймпада (Add Binding → Gamepad → Left Stick).

**Що не вийшло або забрало найбільше часу:** прив'язки в MoveAction — спершу зробив лише WASD і забув стрілки. Unity один раз вилетів і відновив сцену з резервної копії.

**Що зрозумів про Update і Time.deltaTime:** Update викликається щокадру, тому 0.01f за кадр на 60 і на 300 FPS — це зовсім різна швидкість. Time.deltaTime — час від попереднього кадру, тому 3.0f * Time.deltaTime означає три одиниці за секунду на будь-якому комп'ютері. Без нього на повільному комп'ютері персонаж повзе. Композит сам нормалізує вектор, тому по діагоналі (0.71, 0.71), а не (1, 1).

**Що робитиму далі:** справжній рівень, декорації й фізика, щоб робот не ходив крізь перешкоди.

### 06.10.2026 — Юніт 2: оточення і фізика

**Що зроблено:** нарізав тайлсети Ducko (FloorGrass, FloorGrassToBricksSquare, FloorWaterRoundCorners) на 3 × 3 з Pixels Per Unit = 64, додав тайли на Game Palette і намалював рівень: трава, цегляний майданчик і озеро. Декорації Decoration_1 і Decoration_2 отримали Pivot = Bottom, у Renderer2D — Transparency Sort Mode = Custom Axis (0, 1, 0), у робота — Pivot (0.5, 0) і Sprite Sort Point = Pivot. Декорації, DamageZone (Draw Mode = Tiled, Mesh Type = Full Rect) і робот — префаби в теці Prefabs. Робот має Rigidbody 2D (Gravity Scale = 0, Freeze Rotation Z) і Box Collider 2D на ногах, декорації — лише Box Collider 2D на нижню половину. Рух перенесено у FixedUpdate через rigidbody2d.MovePosition. На Tilemap — Tilemap Collider 2D (Composite Operation = Merge), Composite Collider 2D і Static Rigidbody 2D; тайлам трави й цегли та FirstTile/SecondTile — Collider Type = None. More things to try: тайлмап Foreground з Order in Layer = 10 (цегляна арка, під якою проходить робот) і будівля Decoration_3 з 9-slicing (Draw Mode = Sliced).

**Що не вийшло або забрало найбільше часу:** у вікні Game з Free Aspect кадр ширший за 16:9, і за краями рівня було видно порожнечу — довелося розширити рівень до 36 клітинок. Арка спершу була з тайлсета FloorBlueGrass, але її бірюзовий верх зливався з травою; перемалював цеглою, а центральну плитку арки прибрав, бо вона повністю ховала робота. Ще прибрав порожній NewMonoBehaviourScript і теку _Recovery.

**Що зрозумів про FixedUpdate і колайдери:** transform.position ставить робота просто в ящик в обхід фізики, рушій його виштовхує, а наступного кадру скрипт заштовхує знову — звідси тремтіння. MovePosition просить рушій пересунути тіло, і він сам зупиняє його перед колайдером. FixedUpdate викликається в такт фізики (50 разів на секунду), тому рух там, а ввід читаю в Update, щоб не загубити натискання. Колайдер без Rigidbody 2D рушій вважає нерухомим, тому декораціям тіло не потрібне. Тайлам підлоги Collider Type = None, бо інакше колайдер має кожна плитка і робот застрягає просто на підлозі. Composite з Merge зливає квадрати рідини в один контур, тож робот не чіпляється на стиках.

**Що в рівні вийшло не так, як у плані, і чому:** за планом рівень мав 24 клітинки завширшки, а вийшло 36 — через ширший кадр камери. Арку планував з тайлів «верху стіни», а зробив цеглою, бо інакше її не видно. Аптечки й шкоду від DamageZone відклав до юніту 3: зараз у зони шкоди звичайний колайдер, і робот у неї впирається.

**Що робитиму далі:** юніт 3 — здоров'я, аптечки й тригери; колайдер DamageZone стане тригером.

### 07.10.2026 — Юніт 3: система здоров'я

**Що зроблено:** у PlayerController додав public int maxHealth = 5, приватне currentHealth, властивість health лише для читання і функцію ChangeHealth з Mathf.Clamp і Debug.Log; швидкість винесена в public float speed = 3.0f. Аптечка: спрайт CollectibleHealth (Pixels Per Unit = 64), Box Collider 2D з Is Trigger, скрипт HealthCollectible — додає 1 і зникає, але лише коли health < maxHealth; префаб, три копії на рівні. Префаб DamageZone отримав Is Trigger, Auto Tiling і скрипт DamageZone з OnTriggerStay2D і ChangeHealth(-1). У Rigidbody 2D робота — Sleeping Mode = Never Sleep, після удару робот невразливий (timeInvincible, isInvincible, damageCooldown). More things to try: ultra damage zone — варіант префаба DamageZone з полем damage = 2 і червоним відтінком; slow healing zone — зелена зона, що додає 1 здоров'я раз на секунду, поки здоров'я не повне.

**Що не вийшло або забрало найбільше часу:** спрайт аптечки має 256 × 256 пікселів, і з Pixels Per Unit = 64 вона стала завбільшки 4 клітинки — зменшив префаб через Scale 0.3. Debug.Log(move) з юніту 1 засипав Console щокадру, і рядків зі здоров'ям не було видно — прибрав його. Зона лікування спершу лікувала б миттєво до повного, бо невразливість стосується лише шкоди, тому додав їй власний таймер interval. Під час перевірки робот застряг у куті між двома декораціями — довелося обходити.

**Що зрозумів про тригери, public і private:** звичайний колайдер фізично зупиняє робота, а тригер пропускає крізь себе і лише повідомляє скрипту про дотик (OnTriggerEnter2D — один раз на вході, OnTriggerStay2D — поки робот усередині). Never Sleep потрібен, бо Rigidbody, що стоїть на місці, засинає, і OnTriggerStay2D перестає викликатися — робот стояв би в зоні без шкоди. currentHealth лишив private, щоб його не міг змінити будь-хто; назовні — лише властивість health для читання, а змінювати здоров'я можна тільки через ChangeHealth, де працює Clamp і невразливість. public-поля (maxHealth, speed, timeInvincible, damage) видно в Inspector, і їх можна налаштовувати без коду.

**Яке значення Time Invincible обрав і для якого гравця:** 1.5 с замість 2 з туторіалу. Розраховував на гравця-новачка: у звичайній зоні він втрачає 1 здоров'я раз на півтори секунди, тож з 5 очок має близько 7 секунд, щоб помітити шкоду в Console й вийти. Значення 2 здалося надто поблажливим, а 0.5 забирало все здоров'я майже миттєво.

**Що робитиму далі:** юніт 4 — персонажі й взаємодія.

