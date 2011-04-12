OmegaNET supports a HTML-*like* syntax for formatting text values.  The main difference from HTML is that there is no real relationship between open and close tags, they are just ways to turn on/off various features.  For example: ``<wide>`` turns on wide characters, and ``</wide>`` turns off wide characters.  It is not required, however to properly close ``<wide>`` nor is it necessary to nest properly.

Format
======
``<flash></flash>``
  Turns on/off character flashing.
``<high></high>``
  Turns on/off double-high characters.

    // Call
    //   <date format />
    //   <time/>
    //   <temperature format />
    //   <string +file />
    //   <picture +file />
    //   <animimation mode +files hold />
    //   <counter +id />
    
    // Format
    //   <flash></flash>
    //   <high></high>
    //   <descenders></descenders>
    //   <wide></wide>
    //   <double></double>
    //   <fixed></fixed>
    //   <fancy></fancy>
    //   <shadow></shadow>
    // 
    //   <font +name />
    //   <color +name />
    //   <rgb +font shade />
    
    // Extended character sets
    //    ÇüéâäàåçêëèïîìÅÉæÆô
    //    <extended decimal/hex/name />
    //    euro
    //    up,down,left,right
    //    pacman, boat, ball, telephone, heart, car, handicap, rhino,
    //    mug, satellite, copyright, male, female, bottle, diskette, printer, music, infinity