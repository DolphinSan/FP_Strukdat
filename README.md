# FP_Strukdat 


Aplikasi Daily Task Management  
Aplikasi ini merupakan aplikasi CLI yang digunakan untuk mengelola tugas harian dengan menggunakan struktur data binary search tree dan stack. 

 Use Case and Scenario:
User Story: Sebagai seorang developer, saya ingin mengelola tugas harian dengan sistem prioritas yang jelas. 

Workflow: 
1. Add Task: "Fix login bug" (Priority: 9)
2. Add Task: "Update documentation" (Priority: 3) 
3. Add Task: "Code review" (Priority: 7) 
4. List Tasks → BST akan menampilkan urutan: Fix login bug (9), Code review (7), Update documentation (3) 
5. Complete Task: "Fix login bug" 
6. Undo → Membatalkan completion menggunakan Stack

Menyimpan history setiap perubahan pada daftar tugas
Memungkinkan user untuk membatalkan operasi terakhir (add, delete, update task)
Menyimpan snapshot state aplikasi sebelum operasi penting
Implementasi "Recent Actions" untuk tracking aktivitas user








	5.2  Scenario 2: Project Planning
User Story: Sebagai project manager, saya ingin mengatur tugas tim berdasarkan prioritas dan deadline. 

Workflow: 
1. Bulk import tasks dari file CSV
2. Filter tasks dengan prioritas 8-10 (critical tasks) 
3. Update multiple tasks sekaligus 
4. Accidentally delete important task 
5. Undo deletion menggunakan Stack history 
6. Export filtered high-priority tasks untuk tim
	
	5.3 Scenario 3: Academic Task Management
User Story: Sebagai mahasiswa, saya ingin mengatur tugas kuliah berdasarkan tingkat kepentingan.

Workflow: 
1. Add assignments dengan prioritas berbeda 
2. Search tasks berdasarkan mata kuliah 
3. Mark completed assignments 
4. View statistics: completed vs pending 
5. Undo accidental task deletion 
6. Auto-save untuk backup

Fitur: 
    Task Management Features
Add Task: Menambah tugas baru dengan prioritas
Delete Task: Menghapus tugas berdasarkan ID atau nama
Update Task: Mengubah detail tugas (nama, prioritas, status)
List Tasks: Menampilkan tugas berdasarkan prioritas (menggunakan BST traversal)
Search Task: Mencari tugas berdasarkan prioritas atau nama
Filter Tasks: Menampilkan tugas dalam range prioritas tertentu
	 Undo/Redo System (Stack Implementation)
Undo: Membatalkan operasi terakhir
Redo: Mengulangi operasi yang telah di-undo
History: Menampilkan riwayat operasi terakhir
Clear History: Membersihkan history undo/redo
	  Data Persistence
Save to File: Export tugas ke file JSON
Load from File: Import tugas dari file JSON
Auto-save: Otomatis menyimpan perubahan
Backup System: Membuat backup berkala





	 Utility Features
Statistics: Menampilkan statistik tugas (completed, pending, by priority)
Performance Analysis: Analisis kompleksitas operasi
Bulk Operations: Import/export multiple tasks
Configuration: Settings untuk auto-save, backup interval, dll

