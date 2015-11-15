using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(EnKrypt.Core.Version);
            Console.WriteLine("");

            var value = "";
            Console.WriteLine("Please enter a string:");
            value = Console.ReadLine();
            Console.WriteLine("");

            var alphabet = new EnKrypt.Ciphers.Duotrige.Alphabet().GenerateAlphabet(value);
            Console.WriteLine("Custom Alphabet:");
            Console.WriteLine(new string(alphabet));
            Console.WriteLine("");

            var ciphered = "";
            var CipherManager = new EnKrypt.Ciphers.Duotrige.Cipher();

            Console.WriteLine("Ciphered (Custom Alphabet):");
            ciphered = CipherManager.Encrypt(value, alphabet);
            Console.WriteLine(ciphered);
            //Console.WriteLine(EnKrypt.Ciphers.ShiftN.Core.Condense(ciphered));
            Console.WriteLine("");

            Console.WriteLine("Deciphered:");
            //Console.WriteLine(EnKrypt.Ciphers.ShiftN.ShiftN32.Decipher(ciphered));
            Console.WriteLine(CipherManager.Decrypt(ciphered, alphabet));
            Console.WriteLine("");

            //Console.WriteLine(EnKrypt.Core.Version);
            //Console.WriteLine("");

            //var value = "";
            //Console.WriteLine("Please enter a string:");
            //value = Console.ReadLine();
            //Console.WriteLine("");

            //var alphabet = EnKrypt.Ciphers.ShiftN.Core.NewAlphabet(value);
            //Console.WriteLine("Custom Alphabet:");
            //Console.WriteLine(new string(alphabet));
            //Console.WriteLine("");

            //var ciphered = "";

            //Console.WriteLine("Ciphered (Standard Alphabet):");
            //ciphered = EnKrypt.Ciphers.ShiftN.ShiftN32.Cipher(value);
            //Console.WriteLine(ciphered);
            //Console.WriteLine(EnKrypt.Ciphers.ShiftN.Core.Condense(ciphered));
            //Console.WriteLine("");

            //Console.WriteLine("Deciphered:");
            //Console.WriteLine(EnKrypt.Ciphers.ShiftN.ShiftN32.Decipher(ciphered));
            //Console.WriteLine("");

            //Console.WriteLine("Ciphered (Custom Alphabet):");
            //ciphered = EnKrypt.Ciphers.ShiftN.ShiftN32.Cipher(value, alphabet);
            //Console.WriteLine(ciphered);
            //Console.WriteLine(EnKrypt.Ciphers.ShiftN.Core.Condense(ciphered));
            //Console.WriteLine("");

            //Console.WriteLine("Deciphered:");
            //Console.WriteLine(EnKrypt.Ciphers.ShiftN.ShiftN32.Decipher(ciphered, alphabet));
            //Console.WriteLine("");

            //var text = "Riding a bicycle is a life skill we learn as kids that sticks with us for a lifetime. Once you learn it, you never forget it. But what if there was a special kind of bike that will make everything you learned useless? You may think that I am saying nonsense, but there is actually a bike that nobody can ride unless they unlearn riding a bike. Watch the video and you’ll understand. ";

            //Console.WriteLine("Ciphered:");
            //ciphered = EnKrypt.Ciphers.ShiftN.ShiftN32.Cipher(value);
            //Console.WriteLine(EnKrypt.Ciphers.ShiftN.ShiftN32.Cipher("KEVINWILLIAMS", true));
            //Console.WriteLine(ciphered);

            //value = EnKrypt.Ciphers.ShiftN.ShiftN32.Decipher("133373159");
            //value += EnKrypt.Ciphers.ShiftN.ShiftN32.Decipher("112633131");
            //value += EnKrypt.Ciphers.ShiftN.ShiftN32.Decipher("19");
            //Console.WriteLine(value);

            //Console.WriteLine("New Alphabet:");
            //Console.WriteLine(EnKrypt.Core.NewAlphabet("Everyday I taka a walk in the park with Elisabeth."));
            //Console.WriteLine(EnKrypt.Core.NewAlphabet("Everyday I taka a walk in the park with Elisabeth"));

            ////var text = "Riding a bicycle is a life skill we learn as kids that sticks with us for a lifetime. Once you learn it, you never forget it. But what if there was a special kind of bike that will make everything you learned useless? You may think that I am saying nonsense, but there is actually a bike that nobody can ride unless they unlearn riding a bike. Watch the video and you’ll understand. ";

            //var text = "The Advanced Encryption Standard (AES) In order to test strengthen of any symmetric encryption method. First, you need to determine the method used. For example, AES is a specification for an encryption of e-data established by the U.S. National Institute of Standards and Technology (NIST) in 2001. AES is based on the Rijndael cipher developed by two Belgian cryptographers, Joan Daemen and Vincent Rijmen, who submitted a proposal to NIST during the AES selection process. Rijndael is a family of ciphers with different key and block sizes. For AES, NIST selected three members of the Rijndael family, each with a block size of 128 bits, but three different key lengths: 128, 192 and 256 bits. AES has been adopted by the U.S. government and is now used worldwide. The algorithm described by AES is a symmetric-key algorithm, meaning the same key is used for both encrypting and decrypting. In the United States, AES was announced by the NIST as U.S. FIPS PUB 197 (FIPS 197) on November 26, 2001. This announcement followed a five-year standardization process in which fifteen competing designs were presented and evaluated, before the Rijndael cipher was selected as the most suitable. AES became effective as a federal government standard on May 26, 2002 after approval by the Secretary of Commerce. AES is included in the ISO/IEC 18033-3 standard. AES is available in many different encryption packages, and is the first publicly accessible and open cipher approved by the National Security Agency (NSA) for top secret information when used in an NSA approved cryptographic module.";
            //text += "Until May 2009, the only successful published attacks against the full AES were side-channel attacks on some specific implementations. The National Security Agency (NSA) reviewed all the AES finalists, including Rijndael, and stated that all of them were secure enough for U.S. Government non-classified data. In June 2003, the U.S. Government announced that AES could be used to protect classified information: The design and strength of all key lengths of the AES algorithm (i.e., 128, 192 and 256) are sufficient to protect classified information up to the SECRET level. TOP SECRET information will require use of either the 192 or 256 key lengths. The implementation of AES in products intended to protect national security systems and must be reviewed and certified by NSA prior to their acquisition and use. AES has 10 rounds for 128-bit keys, 12 rounds for 192-bit keys, and 14 rounds for 256-bit keys. By 2006, the best known attacks were on 7 rounds for 128-bit keys, 8 rounds for 192-bit keys, and 9 rounds for 256-bit keys. Known attacks For cryptographers, a cryptographic 'break' is anything faster than a brute force-performing one trial decryption for each key. This includes results that are infeasible with current technology. The largest successful publicly known brute force attack against any block-cipher encryption was against a 64-bit RC5 key by distributed.net in 2006. AES has a fairly simple algebraic description. In 2002, a theoretical attack, termed the 'XSL attack', was announced by Nicolas Courtois and Josef Pieprzyk, purporting to show a weakness in the AES algorithm due to its simple description. Since then, other papers have shown that the attack as originally presented is unworkable.";
            //text += "On July 1, 2009, Bruce Schneier blogged about a related-key attack on the 192-bit and 256-bit versions of AES, discovered by Alex Biryukov and Dmitry Khovratovich, which exploits AES's somewhat simple key schedule and has a complexity of 2119. In December 2009 it was improved to 299.5. This is a follow-up to an attack discovered earlier in 2009 by Alex Biryukov, Dmitry Khovratovich, and Ivica Nikolić, with a complexity of 296 for one out of every 235 keys. Another attack was blogged by Bruce Schneier on July 30, 2009 and released as a preprint on August 3, 2009. This new attack, by Alex Biryukov, Orr Dunkelman, Nathan Keller, Dmitry Khovratovich, and Adi Shamir, is against AES-256 that uses only two related keys and 239 time to recover the complete 256-bit key of a 9-round version, or 245 time for a 10-round version with a stronger type of related sub-key attack, or 270 time for an 11-round version. 256-bit AES uses 14 rounds, so these attacks aren't effective against full AES. In November 2009, the first known-key distinguishing attack against a reduced 8-round version of AES-128 was released as a preprint. This known-key distinguishing attack is an improvement of the rebound or the start-from-the-middle attacks for AES-like permutations, which view two consecutive rounds of permutation as the application of a so-called Super-Sbox. It works on the 8-round version of AES-128, with a time complexity of 248, and a memory complexity of 232. In July 2010 Vincent Rijmen published an ironic paper on 'chosen-key-relations-in-the-middle' attacks on AES-128. The first key-recovery attacks on full AES were due to Andrey Bogdanov, Dmitry Khovratovich, and Christian Rechberger, and were published in 2011.";
            //text += "The attack is a biclique attack and is faster than brute force by a factor of about four. It requires 2126.1 operations to recover an AES-128 key. For AES-192 and AES-256, 2189.7 and 2254.4 operations are needed, respectively.";
            //text += "Side-channel attacks Side-channel attacks do not attack the underlying cipher, and thus are not related to security in that context. They rather attack implementations of the cipher on systems which inadvertently leak data. There are several such known attacks on certain implementations of AES. In April 2005, D.J. Bernstein announced a cache-timing attack that he used to break a custom server that used OpenSSL's AES encryption. The attack required over 200 million chosen plaintexts. The custom server was designed to give out as much timing information as possible (the server reports back the number of machine cycles taken by the encryption operation); however, as Bernstein pointed out, 'reducing the precision of the server's timestamps, or eliminating them from the server's responses, does not stop the attack: the client simply uses round-trip timings based on its local clock, and compensates for the increased noise by averaging over a larger number of samples.' In October 2005, Dag Arne Osvik, Adi Shamir and Eran Tromer presented a paper demonstrating several cache-timing attacks against AES. One attack was able to obtain an entire AES key after only 800 operations triggering encryptions, in a total of 65 milliseconds. This attack requires the attacker to be able to run programs on the same system or platform that is performing AES. In December 2009 an attack on some hardware implementations was published that used differential fault analysis and allows recovery of a key with a complexity of 232. In November 2010 Endre Bangerter, David Gullasch and Stephan Krenn published a paper which described a practical approach to a 'near real time' recovery of secret keys from AES-128 without the need for ";
            //text += "either cipher text or plaintext. The approach also works on AES-128 implementations that use compression tables, such as OpenSSL. Like some earlier attacks this one requires the ability to run unprivileged code on the system performing the AES encryption, which may be achieved by malware infection far more easily than commandeering the root account.";
            //text += "NIST/CSEC validation The Cryptographic Module Validation Program (CMVP) is operated jointly by the United States Government's National Institute of Standards and Technology (NIST) Computer Security Division and the Communications Security Establishment (CSE) of the Government of Canada. The use of cryptographic modules validated to NIST FIPS 140-2 is required by the United States Government for encryption of all data that has a classification of Sensitive but Unclassified. Instead, FIPS 197 validation is typically just listed as an 'FIPS approved: AES' notation (with a specific FIPS 197 certificate number) in the current list of FIPS 140 validated cryptographic modules. The Cryptographic Algorithm Validation Program (CAVP) allows for independent validation of the correct implementation of the AES algorithm at a reasonable cost. Successful validation results in being listed on the NIST validations page. This testing is a pre-requisite for the FIPS 140-2 module validation described below. However, successful CAVP validation in no way implies that the cryptographic module implementing the algorithm is secure. A cryptographic module lacking FIPS 140-2 validation or specific approval by the NSA is not deemed secure by the US Government and cannot be used to protect government data. FIPS 140-2 validation is challenging to achieve both technically and fiscally. There is a standardized battery of tests as well as an element of source code review that must be passed over a period of a few weeks. The cost to perform these tests through an approved laboratory can be significant (e.g., well over $30,000 US) and does not include the time it takes to write, test, document and prepare a module for validation. ";
            //text += "After validation, modules must be re-submitted and re-evaluated if they are changed in any way. This can vary from simple paperwork updates if the security functionality did not change to a more substantial set of re-testing if the security functionality was impacted by the change.";
            //text += "Performance High speed and low RAM requirements were criteria of the AES selection process. Thus AES performs well on a wide variety of hardware, from 8-bit smart cards to high-performance computers. On a Pentium Pro, AES encryption requires 18 clock cycles per byte, equivalent to a throughput of about 11 MB/s for a 200 MHz processor. On a 1.7 GHz Pentium M throughput is about 60 MB/s. On Intel Core i3/i5/i7 and AMD APU and FX CPUs supporting AES-NI instruction set extensions, throughput can be over 700 MB/s per thread.";

            //Console.WriteLine(EnKrypt.Core.NewAlphabet(text).Length);
            //Console.WriteLine(EnKrypt.Core.NewAlphabet(text));

            //var testInt = 511;

            //var baseConverter = new EnKrypt.Common.BaseConvert(32);

            //Console.WriteLine("Testing:" + testInt);
            //Console.WriteLine(baseConverter.ToTargetBase(testInt));
            //Console.WriteLine("");

            //testInt = 255;

            //Console.WriteLine("Testing:" + testInt);
            //Console.WriteLine(baseConverter.ToTargetBase(testInt));
            //Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("Hit any key to continue...");
            Console.ReadKey();
        }
    }
}
