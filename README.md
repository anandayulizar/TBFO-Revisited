##Cara Menjalankan Program
1. Buka TBFO-Revisited.sln menggunakan Visual Studio versi tebaru (kalau engga kadang2 gabisa jalan karena break mode)
2. Pastikan Nuget Package yang dibutuhkan sudah ditambahkan dalam Visual Studio (harusnya sudah sih, diantaranya:
    -AutomaticGraphLayout
    -AutomaticGraphLayout.Drawing
    -AutomaticGraphLayout.GraphViewerGDI
) 
3. Jangan menggunakan Msagl Package untuk menggantikan AutomaticGraphLayout karena salah satu kelas yang digunakan (ToolBox) sudah deprecated 
(asumsikan versi .NET Core tidak lebih jadul dari 3.1)
4. Buka Form.cs dan run code

##Fitur Program
1. Menerima string regex dengan syntax pada umumnya (spasi akan diabaikan oleh program), dengan urutan operasi paling duluan *, concat, +
2. Karakter yang tersedia hanya e, 0, 1. Apabila ingin mengganti menambah dapat ganti variabel characters pada Form1.cs menjadi "e012" misalnya
3. Saat submit ditekan, bakal muncul gambar eNFA dan DFA dari Regex yang ada. Accepted state diwarnai kuning, state awal pasti 0
4. Silakan input string pada textArea kedua dan setiap dimasukkan karakter baru/ dihapuskan, 
graph akan menunjukkan jalan dengan mewarnai panah berwarna merah. State yang sekarang dapat dikunjungi akan memiliki outline merah
5. Dapat juga secara manual menambahkan/mengurangi final state pada tombol Add/Remove dengan terlebih dahulu memilih state mana yang mau diubah
6. Dapat juga secara manual menambahkan edge eNFA pada tombol Add Edge, dengan mengisi dahulu properti edgenya
7. Error handling saya ccd, jadi tolong masukkan Regex dan Textnya menggunakan karakter yang benar

Semoga tidak ada masalah dalam menjalankan program, terima kasih