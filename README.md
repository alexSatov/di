﻿# Dependency Injection Container

## Самостоятельная подготовка

Посмотрите видеолекцию [DI-контейнеры](https://ulearn.me/Course/cs2/Vviedieniie_93d19beb-1465-430f-ac12-03f40ebd3e17) (~1.5 часов)

## Практика работы с DI-контейнером

1. Разминка. В классе Program переделайте Main так, чтобы MainForm 
создавался контейнером. Удалите у MainForm конструктор без параметров 
и сделайте так, чтобы контейнер инжектировал в MainForm список IUiAction.

2. INeed<T>. Изучите код KochFractalAction. 
Изучите механику работы INeed<T> и DependencyInjector.
Оцените такой подход к управлению зависимостями.


3. Рефакторинг. Измените класс KochFractalAction так, 
чтобы его зависимости IImageHolder и Pallette инжектировались 
явно через конструктор, без использования интерфейса INeed.

  Подсказка. Сложность в том, чтобы в MainForm и KochFractalAction 
  оказались ссылки на один и тот же объект PictureBoxImageHolder.
  
Убедитесь, что настройка палитры для рисования кривой Коха всё ещё работает.

4. Еще рефакторинг. Изучите KochFractalAction и поймите, что 
на самом деле IImageHolder и Pallette ему не нужны. Измените его так,
чтобы он принимал только KochPainter. 

5. Фабрика. Аналогично удалите INeed, 
и явное использование контейнера из класса DragonFractalAction.
Дополнительное ограничение — нельзя менять публичный интерфейс DragonPainter.
Особенность в том, что одна из зависимостей DragonPainter — 
DragonSettings оказывается известной только в процессе работы экшена.
Из-за этого вы не можете просить инжектировать в конструктор уже готовый Painter.
Вместо этого инжектируйте фабрику DragonPainter-ов.
https://github.com/ninject/Ninject.Extensions.Factory/wiki/Factory-interface

6. Фабрика 2. Преобразуйте DragonSettingsGenerator также в фабрику 
и инжектируйте эту зависимость в DragonFractalAction.

7. Новая зависимость. Переведите DragonPainter на использование цветов палитры, 
как это сделано в KochPainter. 

Убедитесь, что экшен настройки палитры работает как надо.
Если вы всё сделали правильно, то для добавления зависимости вам не пришлось 
править код работы с контейнером вообще. Магия!

8. Источник зависимости. Аналогично отрефакторите ImageSettingsAction.
Попробуйте придумать, как избавиться от класса IImageSettingsProvider.

Убедитесь, что окно настройки размера изображения запоминает установленный размер.

9. Избавьтесь от остальных использований INeed и удалите этот интерфейс 
и класс DependencyInjector из проекта.

10. Обратите внимание на многочисленные привязки к IUiAction. В реальных
проектах количество классов может исчисляться десятками и сотнями. Воспользуйтесь
документацией https://github.com/ninject/Ninject.Extensions.Conventions 
и найдите, как все эти привязки сделать в одну строчку. 

11. Кажется после последнего рефакторинга пункты меню перемешались. 
Что делать?