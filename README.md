# Absence Duration Kata

The goal of this kata is to calculate the total number of working days used for a holiday booking when working different rotas.

### For example:  Assuming a ‘Monday to Friday’ shift plan

    Days:     MTWTFSS MTWTFSS MTWTFSS
    
    Shifts:   XXXXX00 XXXXX00 XXXXX00
    Booking:  X
    Duration: 1
    
    Shifts:   XXXXX00 XXXXX00 XXXXX00
    Booking:  XXXXXXX
    Duration: 5
    
    Shifts:   XXXXX00 XXXXX00 XXXXX00
    Booking:  XXXXXXX XXXXXXX
    Duration: 10
    
    Shifts:   XXXXX00 XXXXX00 XXXXX00
    Booking:     XXX
    Duration: 2
    
    Shifts:   XXXXX00 XXXXX00 XXXXX00
    Booking:    XXXXX XX
    Duration: 5

### For example:  Assuming a ‘4 on 4 off’ shift plan

    Days:     MTWTFSS MTWTFSS MTWTFSS
    
    Shifts:   XXXX000 0XXXX00 00XXXX0
    Booking:  X
    Duration: 1
    
    Shifts:   XXXX000 0XXXX00 00XXXX0
    Booking:  XXXXXXX
    Duration: 4
    
    Shifts:   XXXX000 0XXXX00 00XXXX0
    Booking:  XXXXXXX XXXXXXX
    Duration: 8
    
    Shifts:   XXXX000 0XXXX00 00XXXX0
    Booking:     XXX
    Duration: 1
    
    Shifts:   XXXX000 0XXXX00 00XXXX0
    Booking:    XXXXX XX
    Duration: 3

