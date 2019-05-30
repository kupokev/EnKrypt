# EnKrypt
Custom Cipher library that I am just building to play with different ways of manipulating data.

## Usage

    // Set password and message
    var password = "Testing#158";
    var message = "This is a random test message";

    // Show original message
    Console.WriteLine("Original Message");
    Console.WriteLine(message);

    // Generate key from password
    var key = EnKrypt.Rubyk.GetKey(password);

    // Encrypt the message
    var cipher = EnKrypt.Rubyk.Encrypt(message, key);

    Console.WriteLine(Environment.NewLine + "Encrypted Message");
    Console.WriteLine(cipher);

    // Decrypt the message
    Console.WriteLine(Environment.NewLine + "Decrypted Message");
    Console.WriteLine(EnKrypt.Rubyk.Decrypt(cipher, key));

## Ciphers

#### Rubyk

The shift was done by first converting characters to their hex value. Once converted to hex, the individual hex values would shift 1 character and then converted bac into characters.

For example:

    Kupo = 4B 75 70 6F

Would convert to the following

    B7 57 06 F4


#### Cubes

The cubes are a minimum of 3x3x3. The cubes expand to a size that will contain enough elements to fit each character of the text. So a string up to 27 character will fit in a 3x3x3. Between 28 and 64 the cube will be a 4x4x4.

If there are any empty elements in the cube after the plain text has been populated, the rest of the elements will be populated with a character "0". This is so that no element is left empty.

When populated, a cube should look something like the below text

Text: Testing a short string

    Layer 0:

        T  t  g

        0  o  0

        r  g  0

    Layer 1:

        e  i  0
        
        s  r  s
        
        i  0  0


    Layer 2:

        s  n  a
        
        h  t  t
        
        n  0  0

