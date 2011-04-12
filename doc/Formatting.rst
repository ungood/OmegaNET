OmegaNET supports a HTML-*like* syntax for formatting text values.  The main difference from HTML is that there is no real relationship between open and close tags, they are just ways to turn on/off various features.  For example: ``<wide>`` turns on wide characters, and ``</wide>`` turns off wide characters.  It is not required, however to properly close ``<wide>`` nor is it necessary to nest properly. Also, elements can be shortened to their shortest, unique name.  For example, ``<desc>`` or ``<de>`` will produce the same results as ``<descenders>``.

[attributes] are optional.

Format
======
``<flash></flash>``
  Turns on/off character flashing.
``<high></high>``
  Turns on/off double-high characters.
``<descenders></descenders>``
  Turns on/off true descenders.
``<wide></wide>``
  Turns on/off wide characters.
``<double></wide>``
  Turns on/off double-wide characters.
``<fixed></fixed>``
  Turns on/off fixed-width characters.
``<fancy></fancy>``
  Turns on/off fancy characters.
``<shadow></shadow>``
  Turns on/off shadowed characters.
``<auxiliary></auxiliary>``
  Turns on/off the auxiliary port (Series 4000/7000).
``<font name>``
  Sets the font to one of: **TODO**
``<color name>``
  Sets the color to one of: **TODO**
``<rgb font [shade]>``
  Sets the character color via RGB values.  **TODO**

References
==========
``<date [format]/>``
  Inserts the current date, using an optional format specifier: **TODO**
``<time/>``
  Inserts the current time.
``<temperature units/>``
  Inserts the temperature using the specified units: (C)elsius/(F)arenheit. Defaults to Celsius.
``<string file/>``
  Inserts the contents of the specified STRING file.
``<picture file/>``
  Inserts the contents of the specified PICTURE file.
``<animimation [mode] files [hold] />``
  Creates an animation.  **TODO**
``<counter id/>
  Inserts the current value of the specified counter.

Extended Characters
===================
Supported international characters can be inserted with the appropriate unicode character:

ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥℞ƒáíóúñÑªº¿°¡ øØćĆčČđÐŠžŽΒšβÁÀÃãÊÍÕõ€ ↑↓←→

Other supported characters can be called using HTML entity syntax:
* ``&lt; &gt; &amp;``
* ``&euro;``
* ``&up; &down; &left; &right;``
* pacman, boat, ball, telephone, heart, car, handicap, rhino, mug, satellite, copyright, male, female, bottle, diskette, printer, music, infinity

**TODO**