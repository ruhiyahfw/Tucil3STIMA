# **Tugas Kecil III Strategi Algoritma**
## Implementasi Algoritma A* untuk Menentukan Lintasan Terpendek

**Deskripsi Program**
========================
- Program ini merupakan program untuk aplikasi desktop sederhana yang digunakan untuk menunjukkan lintasan terpendek dari 2 tempat asal dan tujuan yang dipilih. Pencarian lintasan terpendek ini menggunakan algoritma A * 
- Map yang akan digunakan disediakan dalam bentuk txt yang terdapat di file test, berisi banyak simpul, koordinat setiap simpul beserta nama simpul, dan adjacency matrix.

**Algoritma Program**
========================
Algoritma A* merupakan algoritma pencarian path yang mengkombinasikan *Uniform Cost Search* dan *Greedy-Best First Search* sehingga memiliki fungsi evaluasi (evaluation function) f(n) = g(n)+h(n) di mana g(n) merupakan jarak dari simpul akar ke simpul saat ini dan h(n) adalah perkiraan jarak dari simpul saat ini ke simpul tujuan. Pengaplikasian algoritma A* dalam program kami dapat dilihat di kelas astarclass, yaitu method astar (kode program tersimpan di folder src dan kelas astarclass ada di program.cs)

**Requirement Program dan Instalasi**
========================
- Menginstall Visual Studio 2019
- Pastikan Visual Studio terinstal dengan kakas .NET dan MSAGL.
- Program dijalankan pada sistem operasi Windows.

**Petunjuk Penggunaan**
========================
- Program dapat dijalankan dengan melakukan double klik pada file "tucil3_0404.exe" yang terdapat pada folder "bin" atau dengan menjalankan file program "tucil3_0404.sln" yang terdapat pada folder "src"
- Setelah berhasil dijalankan akan muncul sebuah Windows Forms.
- Pada Windows Forms diinputkan nama peta yang ingin dibaca, untuk melihat peta apa saja  yang tersedia dapat menekan tombol panah ke bewah pada combobox yang tersedia.
- Setelah dipilih, tekan tombol "Submit". Lalu akan muncul visualisasi graf dari peta yang dibaca dan combobox tempat memasukkan simpul asal dan simpul tujuan.
- Masukkan simpul asal dan simpul tujuan yang akan dicari rute terdekatnya menggunakan algoritma A*
- Setelah itu tekan tombol "Explore!!!", lalu akan ditampilkan jalur dari simpul asal ke simpul tujuan dan jarak total yang ditempuh.
- **PERHATIKAN** Input nama peta dan nama simpul harus dipilih dari combobox, tidak ditulis secara manual.

**Author**
========================
1. Ruhiyah Faradishi Widiaputri 13519034 (K01)
2. Nabila Hannania 13519097 (K02)
