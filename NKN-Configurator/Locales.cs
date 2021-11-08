
using System.Collections.Generic;

namespace NKN_Configurator {
	public partial class Form1 {
		private static Dictionary<Lang, Dictionary<string, string>> locales_ = new Dictionary<Lang, Dictionary<string, string>>() {
			{ Lang.EN, new Dictionary<string, string>() {
				{ "boolDisabled", "Disabled" }
				, { "boolEnabled", "Enabled" }
				, { "boolTrue", "True" }
				, { "boolFalse", "False" }
				, { "Proceed", "Proceed" }
				, { "Finish", "Save" }
				, { "Back", "Back" }
				, { "SetDefault", "Set to default" }
				, { "savedSuccesfully", "Configuraton file saved succesfully!" }

				, { "TimeMult", "Time speed multiplier" }
				, { "helpTimeMult", "The ingame time multiplier. Might be unstable.\r\n" +
					"\r\nThe value 0.75 will make one ingame day equal to 10 real life minutes. The value 0.00521 will make one ingame day last for 24 real life hours (roughly)." +
					"\r\nValues beyond 2 will cause npc's to skip their posts, making the game nearly unplayable. Beyond 4 are absolutely unplayable. Values below 0.001 are considered invalid and the default value (1) will be set." }
				
				, { "TimeMultDefault", "Time speed multiplier default value" }
				, { "helpTimeMultDefault", "This will be the default value for TimeMult, meaning that you can switch between TimemultDefault and TimeMult when using TimeScaleSwitchKey. Default value is 1." }
							
				, { "SleepTimeMult", "Sleeping time speed multiplier" }
				, { "helpSleepTimeMult", "Multiplies the sleeping speed, which is 2240 times faster than real life (24 hours would take 38.6 seconds). Therefore, if you set this parameter to 2, it will take 19.3 seconds to sleep for 24 ingame hours." }
				
				, { "UnconditionalSleep", "Unconditional sleep" }
				, { "helpUnconditionalSleep", "Allows you to sleep in bed with full EP and HP. If your EP is full, you'll lose 5 EP, therefore you'll sleep as long as you need to restore these 5 EP." }
				
				, { "InflationAmount", "Inflation multiplier" }
				, { "helpInflationAmount", "The multiplier of the amount of coins that is added when you buy a significant number of the same item from a vendor." +
					"\r\n\r\n0 means that the price will stay the same (10 berry pies will cost you solid 35*10 = 3 silver and 50 copper coins)" +
					"\r\nNegative numbers will make you feel a wholesale buyer (yet don't make it too low, I recommend to keep it beyond -1)" }
				
				, { "DullInventoryMusic", "Dulls tabs GUI music" }
				, { "helpDullInventoryMusic", "Dulls the music while you're in tabs-GUI (inventory/tech/quests/map)." +
					"\r\nThe option is for those who just miss the feeling that the game is paused." }
				
				, { "DefaultSpeed", "Walking speed multiplier" }
				, { "helpDefaultSpeed", "Your character's walking speed multiplier.\r\nValues beyond 3 are not recommended." }
				
				, { "SprintSpeed", "Sprinting speed multiplier" }
				, { "helpSprintSpeed", "Your sprinting speed multiplier (normally it's higher than DefaultSpeed, since it applies to the original speed, not the DefaultSpeed-ed speed)." +
					"\r\nValues beyond 5 are not recommended." }

				, { "SprintKey", "Key for sprint" }
				, { "helpSprintKey", "The key that activates Sprinting.\r\n" +
					"I provide a short list here, but by editing the cfg manually you can set any value that is a valid Unity KeyCode enum constant name." }
				
				, { "SprintToggle", "Toggle sprinting" }
				, { "helpSprintToggle", "By default, you need to press the SprintKey to sprint. Enable this to toggle sprinting with SprintKey.\r\n\r\n" +
					"Do not use this and zero energy cost to just sprint all the time. It could have a performance impact. If you want to continously run fast, just change your default speed using the corresponding multiplier." }
				
				, { "EnergyForSprint", "Sprint energy cost (per second)" }
				, { "helpEnergyForSprint", "The amount of energy you lose each second while sprinting." }
				
				, { "DmgMult", "Player's taken damage multiplier" }
				, { "helpDmgMult", "The multiplier of damage taken by player only. Applies AFTER armor check, so when you wear armor, you take proportional damage to what you'd get without this mult.\r\n\r\n" +
					"greater than 1 - more damage (harder)\r\n" +
					"less than 1 - less damage (easier)\r\n" +
					"0 or less - invincibility" }
				
				, { "GlobalDmgMult", "Global damage multiplier" }
				, { "helpGlobalDmgMult", "The multiplier of damage taken by all entities. Applies BEFORE armor check, so the values lower than 1 will make armor in general stronger, and with values higher than 1 hits will ignore more armor." +
					"\r\n\r\ngreater than 1 - more damage (fights are faster, armors are less effective)\r\n" +
					"less than 1 - less damage (fights are longer, armors are harder)\r\n" +
					"0 or less - invincibility. For all beings. Which means that the game will be unplayable." }
				
				, { "RegenMult", "Health restoration multiplier." }
				, { "helpRegenMult", "Affects health restored at night and consumables." +
					"\r\n\r\ngreater than 1 - more health restored" +
					"\r\nless than 1 - less health restored" +
					"\r\n0 or less - forbidden (the default value will be set)" }

				, { "HealthRegen", "Health regen over time" }
				, { "helpHealthRegen", "Toggles health regen over time." }
				
				, { "HealIfTired", "Apply regen over time if tired" }
				, { "helpHealIfTired", "By default, your health will stop regenerating over time if your energy level is below 10 points." +
					"\r\nEnable this option if you want to always receive healing, even if you're out of energy." }
				
				, { "HealthRegenPerSecond", "Regen over time amount" }
				, { "helpHealthRegenPerSecond", "Health points you regenerate per second when regen over time is enabled." }

				, { "EnergyDrainMult", "Energy drain multiplier" }
				, { "helpEnergyDrainMult", "Multiplies the amount of energy you spend on activities." +
					"\r\n\r\ngreater than 1 - faster depletion (you become tired faster)" +
					"\r\nless than 1 - slower depletion" +
					"\r\n0 or less - no energy drain" }
				
				, { "EnergyReplenMult", "Energy replenishment multiplier" }
				, { "helpEnergyReplenMult", "Multiplies the amount of energy you restore with consumables and during sleep." +
					"\r\n\r\ngreater than 1 - faster replenishment (also, you sleep less)" +
					"\r\nless than 1 - slower replenishment (also, you need more sleep)" +
					"\r\n0 or less - forbidden (the default value will be set)" }

				, { "CraftingSpeed", "Crafting speed multiplier" }
				, { "helpCraftingSpeed", "Crafting speed multiplier (workstations only). If you want crafting to be nearly instant, use some value near 20." }
				
				, { "InteractionSpeed", "Interaction speed multiplier" }
				, { "helpInteractionSpeed", "Affects all interactions in the game. Not fully tested yet." +
					"\r\nMultiplies the Crafting Speed Mult as well, so there's no point in high CraftingSpeed if you've already set InteractionSpeed to some high value." +
					"\r\nValues greater than 5 will make interactions really fast, values beyond 10 are not recommended." }

				, { "OrbsMult", "Tech orb drop multiplier" }
				, { "helpOrbsMult", "Multiplies the amount of tech orbs dropped." }
				
				, { "RoundDown", "Round down the amount of tech orbs" }
				, { "helpRoundDown", "By default, after applying the Tech orb drop mult, if the result is not integer the amount of orbs to drop will be rounded up." +
					"\r\nEnable this option to round the orb amount down." }

				, { "OrbsConstant", "Additional Red tech orbs drop" }
				, { "OCGreen", "Additional Green orbs drop" }
				, { "OCBlue", "Additional Blue orbs drop" }
				, { "helpOrbsConstant", "A constant addition to every type of dropped orbs. Applies before OrbsMult." +
					"\r\n\r\nThe values are Red:Green:Blue. Only integer values are valid (no dots!)" +
					"\r\nNegative values are valid: in that case, orbs will be subtracted from drop amount (yet they won't be taken back from Keeper)." +
					"\r\n\r\nExample: let's say, we have the following settings:" +
					"\r\nOrbs Mult is 1.5 \\ RoundDown is Enabled \\ Orbs additions are 2 : -2 : 5 \\ Applying additions to zero is Disabled" +
					"\r\nAfter chopping a tree that gives you 1 red and 1 green orb, you will receive:" +
					"\r\n(1 + 2) * 1.5 = 4.5, which rounds to 4 red orbs" +
					"\r\n(1 - 2) * 1.5 = -1.5, but we cannot take orbs from you, so you'll just get 0 green orbs" +
					"\r\nblue orbs won't drop since OrbsConstAddIfZero is set to false" }

				, { "OrbsConstAddIfZero", "Apply additions to zero" }
				, { "helpOrbsConstAddIfZero", "By default, 'Tech orb drop addition' won't be applied to orb types that weren't dropped at all." +
					"\r\n\r\nEnable this option to always apply additions." +
					"\r\n\r\nFor example, when you should get greens and reds only, and your 'Tech orb drop addition' has 2 orbs in blue section, you won't get any blues by default, but you would have got 2 blues if this setting was enabled." }

				, { "TimeScaleSwitchKey", "The key to toggle timescale" }
				, { "helpTimeScaleSwitchKey", "Press this key to toggle between normal timescale and the configured value (thus, between 'default * 1' and 'default * TimeMult')." +
					"\r\n\r\nI provide a short list here, but by editing the cfg manually you can set any value that is a valid Unity KeyCode enum constant name." }

				, { "CraftingSpeedKey", "The key to toggle CraftingSpeed" }
				, { "helpCraftingSpeedKey", "Press this key to toggle between normal craftingSpeed and the configured value (thus, between 'default * 1' and 'default * CraftingSpeed ')." +
					"\r\n\r\nI provide a short list here, but by editing the cfg manually you can set any value that is a valid Unity KeyCode enum constant name." }
				
				, { "ToggleCraftAndInteraction", "Switch both speeds" }
				, { "helpToggleCraftAndInteraction", "If true, both InteractionSpeed and CraftingSpeed will be toggled by using the CraftingSpeedKey. If false, only CraftingSpeed will be toggled."}

				, { "ResetPrayKey", "The key to reset weekly pray count" }
				, { "helpResetPrayKey", "Press this key after praying to reset the pray count, allowing you to pray once again. This allows you to pray numerous times a day, yet only at Pride." +
					"\r\n\r\nI provide a short list here, but by editing the cfg manually you can set any value that is a valid Unity KeyCode enum constant name." }
				
				, { "AddMoneyKey", "The key to add 1 gold coin with" }
				, { "helpAddMoneyKey", "Press this key to add 1 gold coin." +
					"\r\n\r\nI provide a short list here, but by editing the cfg manually you can set any value that is a valid Unity KeyCode enum constant name." }

				, { "AllowSaveEverywhere", "Allow fast save" }
				, { "helpAllowSaveEverywhere", "Allows you to press the key to save the game from anywhere, anytime. The functionality is not properly tested and not really recommended, so this option is disabled by default." }

				, { "SaveGameKey", "The key to fast save" }
				, { "helpSaveGameKey", "Press this key to save your game. Not properly tested and not really recommended.\r\nDo not use this at any scripted scene or anything like that.\r\nAlso, when you will load your fast-saved game, you will start at your home, and ambient visual effects will be shown as if you're in tha location you were at at the momet of save (e.g., if you will save outdoors, you will start in a foggy room)." +
					"\r\n\r\nI provide a short list here, but by editing the cfg manually you can set any value that is a valid Unity KeyCode enum constant name." }
				
				, { "ConfigReloadKey", "The key to reload the mod configuration" }
				, { "helpConfigReloadKey", "Press this key to reload the configuration file. Missing values will be reset to defaults." +
					"\r\n\r\nI provide a short list here, but by editing the cfg manually you can set any value that is a valid Unity KeyCode enum constant name." }

			} },
			{ Lang.RU, new Dictionary<string, string>() {
				  { "boolDisabled", "Выкл" }
				, { "boolEnabled", "Вкл" }
				, { "boolTrue", "True" }
				, { "boolFalse", "False" }
				, { "Proceed", "Далее" }
				, { "Finish", "Сохранить" }
				, { "Back", "Назад" }
				, { "SetDefault", "По умолчанию" }
				, { "savedSuccesfully", "Файл конфигурации успешно сохранён!" }

				, { "TimeMult", "Множитель хода времени" }
				, { "helpTimeMult", "Множитель хода времени в игре. Безопасен в основной игре, но может быть причиной багов в длинных заскриптованных сценах.\r\n" +
					"\r\nПри значении 0.75 один день в игре будет длиться 10 минут. При значении 0.00521 масштаб времени в игре сравняется с реальным (полные сутки в игре будут длиться 24 часа)." +
					"\r\nПри значениях более 2 некоторые персонажи будут не успевать доходить до своих постов, из-за чего полноценно играть будет невозможно. При значениях более 4 играть будет совершенно невозможно." +
					"\r\nЗначения менее 0.001 недопустимы, в таком случае будет использовано значение по умолчанию (1)." }
				
				, { "TimeMultDefault", "Множитель хода времени, значение по умолчанию" }
				, { "helpTimeMultDefault", "Это будет значение по умолчанию для TimeMult, что означает, что вы можете переключаться между TimemultDefault и TimeMult при использовании TimeScaleSwitchKey. Значение по умолчанию — 1." }
				
				, { "SleepTimeMult", "Множитель хода времени во сне" }
				, { "helpSleepTimeMult", "Множитель хода времени, когда персонаж спит. По умолчанию (т.е. если установить значение 1) время во сне летит в 2240 раз быстрее, чем в жизни (24 часа пролетят за 38.6 сек)." +
					" Соответственно, если установить значение 2, то сутки во сне пролетят за 19.3 сек." }
				
				, { "UnconditionalSleep", "Разрешить спать отдохнувшим" }
				, { "helpUnconditionalSleep", "Позволяет ложиться спать и сохраняться с полными очками энергии и здоровья. Будет отнято 5 EP, поэтому сон продлится столько, сколько нужно для их восстановления." }

				, { "InflationAmount", "Множитель наценки" }
				, { "helpInflationAmount", "Этот множитель изменяет размер наценки при покупке большого числа одного и того же предмета." +
					"\r\n\r\nПри значении 0 цена одной единицы предмета не будет зависеть от покупаемого количества этого предмета (10 ягодных пирогов будут стоить 35*10 = 3 серебра 50 бронзы)" +
					"\r\nПри отрицательных значениях оптовые закупки будут выгоднее (но всё же, если боитесь багов, лучше выставлять значение между 0 и -1)" }
				
				,{ "DullInventoryMusic", "Приглушать музыку в tab-меню" }
				, { "helpDullInventoryMusic", "Приглушает музыку в меню инвентаря/технологий/квестов/карты." +
					"\r\nОпция для успокоения тех, кто параноит, что игра не встала на паузу, когда они открыли какое-либо меню." }
				
				, { "DefaultSpeed", "Множитель скорости ходьбы" }
				, { "helpDefaultSpeed", "Изменяет обычную скорость передвижения.\r\nСо значениями выше 3 играть может быть некомфортно (когда перемещение будет более чем в три раза быстрее стандартного)." }
				
				, { "SprintSpeed", "Множитель скорости бега" }
				, { "helpSprintSpeed", "Скорость бега относительно стандартной (именно родной-стандартной, «Множитель скорости ходьбы» в расчёте не участвует)." +
					"\r\nСо значениями выше 7 играть может быть некомфортно." }

				, { "SprintKey", "Клавиша бега" }
				, { "helpSprintKey", "Клавиша, которой будет активироваться бег.\r\nМожно установить любое значение, являющееся корректным именем кнопки в перечислении Unity KeyCode (чтобы получить полный список этих самых значений, загуглите «Unity KeyCode»)." }
				
				, { "SprintToggle", "Переключение режима бега" }
				, { "helpSprintToggle", "По умолчанию, чтобы бежать, нужно удерживать клавишу бега. Если же включить эту опцию, то клавиша бега будет переключать режим бега.\r\n\r\n" +
					"Если вы хотите постоянно быстро бегать, то нежелательно включать эту опцию и постоянно держать бег включённым с нулевым расходом энергии на бег. Лучше просто измените скорость ходьбы." }
				
				, { "EnergyForSprint", "Расход энергии на бег (в сек)" }
				, { "helpEnergyForSprint", "Сколько очков энергии вы теряете в секунду во время бега." }

				, { "DmgMult", "Множитель урона игроку" }
				, { "helpDmgMult", "Множитель урона, получаемого игроком. Применяется ПОСЛЕ вычета очков брони, поэтому игрок будет всегда получать урон, пропорциональный тому, какой он получил бы без мода.\r\n\r\n" +
					"Значения больше 1 - увеличенный урон\r\n" +
					"менее 1 (дробные числа) - уменьшенный урон\r\n" +
					"0 и меньше - неуязвимость" }
				
				, { "GlobalDmgMult", "Глобальный множитель урона" }
				, { "helpGlobalDmgMult", "Множитель урона, получаемого всеми существами. Применяется ДО вычета очков брони, поэтому низкие значения повышают пользу от брони, а высокие - снижают." +
					"\r\n\r\nЗначения больше 1 - увеличенный урон (броня приносит меньше пользы)\r\n" +
					"менее 1 (дробные числа) - уменьшенный урон (броня приносит больше пользы)\r\n" +
					"0 и меньше - недопустимые значения (будет использовано значение по умолчанию)" }

				, { "RegenMult", "Множитель восстановления здоровья" }
				, { "helpRegenMult", "Множитель, применяемый к количеству здоровья, восполняемому во сне и при использовании предметов." +
					"\r\n\r\nЗначения больше 1 - восстанавливается больше здоровья" +
					"\r\nменее 1 (дробные числа) - Восстанавливается меньше здоровья" +
					"\r\n0 и меньше - недопустимые значения (будет использовано значение по умолчанию)" }

				, { "HealthRegen", "Постепенное восстановление здоровья" }
				, { "helpHealthRegen", "Активирует постепенное восстановление здоровья когда вы бодрствуете." }
				
				, { "HealIfTired", "Восстанавливать здоровье при усталости" }
				, { "helpHealIfTired", "По умолчанию здоровье не будет постепенно восстанавливаться, если у вас менее 10 очков энергии." +
					"\r\nВключите эту опцию, если хотите, чтобы здоровье восстанавливалось независимо от уровня энергии." }
				
				, { "HealthRegenPerSecond", "Кол-во HP, восстанавливаемых в сек" }
				, { "helpHealthRegenPerSecond", "Количество очков здоровья, восстанавливаемое каждую секунду при включённом «постепенном восстановлении здоровья». НЕ увеличивается под действием «множителя восстановления здоровья»." }

				, { "EnergyDrainMult", "Множитель затрачиваемой энергии" }
				, { "helpEnergyDrainMult", "Множитель, применяемый к количеству энергии, затрачиваемому на всевозможные действия." +
					"\r\n\r\nЗначения больше 1 - энергия тратится быстрее" +
					"\r\nменее 1 (дробные числа) - энергия тратится медленнее" +
					"\r\n0 и меньше - энергия вообще не тратится" }
				
				, { "EnergyReplenMult", "Множитель восполняемой энергии" }
				, { "helpEnergyReplenMult", "Множитель, применяемый к количеству энергии, восполняемому во сне и при использовании предметов." +
					"\r\n\r\nЗначения больше 1 - энергия восполняется быстрее (в т.ч. вы меньше спите)" +
					"\r\nменее 1 (дробные числа) - энергия восполняется медленнее (в т.ч. вы дольше спите)" +
					"\r\n0 и меньше - недопустимые значения (будет использовано значение по умолчанию)" }

				, { "CraftingSpeed", "Множитель скорости создания предметов" }
				, { "helpCraftingSpeed", "Множитель, применяемый к скорости работы за различными станками, столами и наковальнями. Используйте значения около 20, чтобы создавать предметы почти мгновенно." }
				
				, { "InteractionSpeed", "Множитель скорости работы" }
				, { "helpInteractionSpeed", "Множитель, применяемый к скорости любой работы. Может быть причиной багов." +
					"\r\nНакладывается на «Множитель скорости создания предметов», поэтому, возможно, вам нужно будет их как-то сбалансировать между собой." +
					"\r\nЗначения выше 10 не рекомендованы." }

				, { "OrbsMult", "Множитель опыта технологий" }
				, { "helpOrbsMult", "Множитель, применяемый к количеству выпадающих шаров опыта технологий." }
				
				, { "RoundDown", "Кол-во опыта округляется вниз" }
				, { "helpRoundDown", "После применения «Множителя опыта технологий» число опыта может получиться не целым, поэтому оно округляется вверх." +
					"\r\nВключите эту опцию, чтобы округлять число выпадающих шаров опыта вниз." }

				, { "OrbsConstant", "Дополнительные шары силы" }
				, { "OCGreen", "Дополнительные шары природы" }
				, { "OCBlue", "Дополнительные шары духовности" }
				, { "helpOrbsConstant", "Постоянная прибавка к определённому типу шаров опыта технологий. Прибавляется ДО наложения «Множителя опыта технологий»." +
					"\r\n\r\nЗначения в файле конфига идут в порядке Красные:Зелёные:Синие. Можно использовать только целые числа." +
					"\r\nДопускаются отрицательные значения: в таком случае указанное число шаров будет вычитаться из изначального." +
					"\r\n\r\nПример: предположим, что у нас такие вот настройки:" +
					"\r\nМнож. опыта: 1.5 \\ Округление вниз включено \\ Дополнительные шары: 2 : -2 : 5 \\ Не добавлять шары, даже если их не было" +
					"\r\nВ таком случае, после повала дерева, с которого мы получили бы 1 красный и 1 зелёный шар, мы в итоге получим:" +
					"\r\n(1 + 2) * 1.5 = 4.5, которые округлятся до 4 красных шаров" +
					"\r\n(1 - 2) * 1.5 = -1.5, т.е. в итоге 0 зелёных шаров (ведь отнять их у персонажа мы не можем)" +
					"\r\nСиние шары не выпадут, т.к. их не было изначально, а настройку на их выпадение в таком случае мы не включили." }

				, { "OrbsConstAddIfZero", "Добавлять шары, даже если их не было" }
				, { "helpOrbsConstAddIfZero", "По умолчанию дополнительные шары опыта определённого типа не будут выпадать в том случае, если шары этого типа технологий не выпадают при выполнении конкретной работы." +
					"\r\n\r\nВключите эту настройку, чтобы всегда выпадали все дополнительные шары опыта." +
					"\r\n\r\nНапример, если после некой работы выпадают только шары силы и духа, а в настройках указано выпадение 2 дополнительных шара природы, то при выключенной данной опции вы не получите этих двух шаров природы - ведь после выполнения работы шары природы не выпадали. Но если вы включите данную опцию, то вам с любой работы, даже если при ней не падают шары природы, будут вдобавок выпадать эти два шара природы." }
				
				, { "TimeScaleSwitchKey", "Кнопка для ускорения времени" }
				, { "helpTimeScaleSwitchKey", "Переключает между обычной скоростью течения времени и настроенным значением (т.е. между 'поумолч * 1' и 'поумолч * TimeMult')." +
					"\r\n\r\n\r\nМожно установить любое значение, являющееся корректным именем кнопки в перечислении Unity KeyCode (чтобы получить полный список этих самых значений, загуглите «Unity KeyCode»)." }
				
				, { "CraftingSpeedKey", "Кнопка для ускорения создания предметов" }
				, { "helpCraftingSpeedKey", "Переключает между обычной скоростью создания предметов и настроенным значением (т.е. между 'поумолч * 1' и 'поумолч * CraftingSpeed')." +
					"\r\n\r\n\r\nМожно установить любое значение, являющееся корректным именем кнопки в перечислении Unity KeyCode (чтобы получить полный список этих самых значений, загуглите «Unity KeyCode»)." }

				, { "ToggleCraftAndInteraction", "Переключение обеих скоростей" }
				, { "helpToggleCraftAndInteraction", "Если это так, скорость взаимодействия и скорость крафта будут переключаться с помощью craftingSpeedKey. Если значение равно false, переключаться будет только скорость крафта."}

				, { "ResetPrayKey", "Кнопка сброса флага проведённой службы" }
				, { "helpResetPrayKey", "Сбрасывает флаг для проверки, что уже была проведена церковная служба, позволяя прочитать молитву ещё раз (но по-прежнему только в день Гордыни)." +
					"\r\n\r\n\r\nМожно установить любое значение, являющееся корректным именем кнопки в перечислении Unity KeyCode (чтобы получить полный список этих самых значений, загуглите «Unity KeyCode»)." }
				
				, { "AddMoneyKey", "Кнопка для добавления денег" }
				, { "helpAddMoneyKey", "Даёт вам 1 золотую монету." +
					"\r\n\r\n\r\nМожно установить любое значение, являющееся корректным именем кнопки в перечислении Unity KeyCode (чтобы получить полный список этих самых значений, загуглите «Unity KeyCode»)." }

				, { "AllowSaveEverywhere", "Разрешить быстрое сохранение" }
				, { "helpAllowSaveEverywhere", "Позволяет сохраниться по нажатию кнопки, где угодно и когда угодно." +
					"\r\n\r\nФункционал сохранения толком не протестирован, поэтому при использовании обязательно делайте резервные копии сохранений.\r\nНе сохраняйтесь посреди заскриптованных сцен и всего такого." +
					"\r\nПри загрузке сохранённой таким образом игры вы начнёте игру у себя дома, но при этом в доме будут погодные визуальные эффекты (напрмер, туман), которые были в той локации, в которой вы сохранились." }

				, { "SaveGameKey", "Кнопка для быстрого сохранения" }
				, { "helpSaveGameKey", "Кнопка для быстрого сохранения." +
					"\r\n\r\nФункционал сохранения толком не протестирован, поэтому при использовании обязательно делайте резервные копии сохранений.\r\nНе сохраняйтесь посреди заскриптованных сцен и всего такого." +
					"\r\nПри загрузке сохранённой таким образом игры вы начнёте игру у себя дома, но при этом в доме будут погодные визуальные эффекты (напрмер, туман), которые были в той локации, в которой вы сохранились." +
					"\r\n\r\n\r\nМожно установить любое значение, являющееся корректным именем кнопки в перечислении Unity KeyCode (чтобы получить полный список этих самых значений, загуглите «Unity KeyCode»)." }
				
				, { "ConfigReloadKey", "Кнопка для перепрочтения файла конфигурации" }
				, { "helpConfigReloadKey", "Нажмите, чтобы заново прочитать файл конфигурации этого мода." +
					"\r\n\r\n\r\nМожно установить любое значение, являющееся корректным именем кнопки в перечислении Unity KeyCode (чтобы получить полный список этих самых значений, загуглите «Unity KeyCode»)." }
			} }
		};
	}
}
