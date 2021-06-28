# TBFO-Revisited
## Latar Belakang
Halo kawan, apakah kalian tahu kalau TBFO itu di bawah lab IRK juga? Aku berani nebak pasti mayoritas dari kalian udah cukup lupa dengan ilmu-ilmu TBFO berhubung di semester 4 gaada yang matkul yang berupa lanjutan dari TBFO. Oleh karena itu, mari kita refresh ingatan kalian mengenai TBFO di sini:)

Intinya, kalian akan membuat sebuah program yang memvisualisasikan Finite Automata dari rangkaian Regular Expressions yang diterima. Spesifikasi detail ada di bawah ini. Kalo kalian lupa itu apa, silahkan buka kitab dingdong dan cari buku TBFO huehehe.

## Spesifikasi Wajib (2000)
1. Program merupakan sebuah aplikasi GUI berbasis desktop menggunakan bahasa <b>python/java/C#</b>. Penggunaan bahasa lain diharapkan menghubungi saya terlebih dahulu. Intinya, program yang dibuat gambarannya seperti saat tubes materi BFS DFS
2. Program dapat menerima input berupa *Regular Expressions*. Contoh: <br>
`01*(10)* + (10)*`<br>
`(e + 1)(01)*(e + 0)`<br>
*Catatan*: 
- Penulisan epsilon dapat menggunakan 'e' untuk mempermudah
- Format penggunaan white space (spasi) dibebaskan tidak harus seperti contoh
3. Program dapat menampilkan visualisasi *Regular Expressions* tersebut dalam bentuk state diagram dari e-NFA.
4. Program dapat menerima input string dan memeriksa apakah string tersebut diterima oleh e-NFA tersebut. Transisi state dalam proses pemeriksaan tersebut ditampilkan pada GUI. (Contoh: highlight state yang sedang dilalui)

## Spesifikasi Bonus
### 1. Membuat e-NFA melalui GUI (700)
Pengguna dapat membuat e-NFA sendiri melalui GUI, tidak hanya melalui input *Regular Expressions*. Misalkan, pengguna dapat membuat beberapa state, lalu mendefinisikan transisi antar state. Alur pembuatan e-NFA melalui GUI dibebaskan.
### 2. Konversi e-NFA menjadi DFA (500)
e-NFA yang telah ditampilkan dapat dikonversikan menjadi DFA dan divisualisasikan. 

## Komponen Penilaian
1. Kebenaran program dan fungsionalitas
2. Algoritma
3. Modularitas
4. Keindahan UI
5. Kreativitas. Kreativitas apapun akan dihargai :D

## Pengerjaan
Fork repository ini dan kerjakan di repository hasil fork kalian. Apabila sudah selesai, dapat melakukan pull request. Sabar ya kawan nanti pasti akan dinilai pada waktunya

Jangan lupa bikin README!! README minimal berisi cara pengerjaan program dan prerequisite untuk menjalankan program. Aku bakal pura-pura bodoh saat meriksa jadi kalo README-nya gaada cara menjalankannya, risiko ditanggung sendiri :D Sumpah gais saat kalian jadi asisten, bakal gatel ngeliat yang README-nya gajelas terus harus nyari-nyari sendiri klo di-nolin kan kasian:)

## Lain-lain
Jika ada pertanyaan, silahkan buat issue github dan pc via line (id line: yulizar57).<br>
Selamat mengerjakan kawan!:D<br>
Jangan lupa kata-kata yang biasanya ada di bawah spek tubes IRK,
```
It's not worth it if you're not having fun!
```

## Referensi
- bit.ly/KitabSuciDingDong cari buku TBFO-nya sendiri ya:D
