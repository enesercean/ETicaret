import { Component, EventEmitter, Input, Output, HostListener } from '@angular/core';
import { NgxFileDropEntry, FileSystemFileEntry } from 'ngx-file-drop';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss'],
  standalone: false
})
export class FileUploadComponent {
  constructor(
    private httpClientService: HttpClientService,
    private toastr: CustomToastrService
  ) { }

  public files: NgxFileDropEntry[] = [];

  @Input() options: Partial<FileUploadOptions>;
  @Input() uid: string;
  @Output() customEvent: EventEmitter<void> = new EventEmitter<void>();


  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();
    let fileCount = files.length;
    let processedCount = 0;

    if (fileCount === 0) return;

    const appendFile = (_file: File, fileEntry: NgxFileDropEntry) => {
      fileData.append(_file.name, _file, fileEntry.relativePath);
      processedCount++;
      if (processedCount === fileCount) {
        this.uploadData(fileData, "Seçilen dosyaları yüklemek istediğinize emin misiniz?");
      }
    };

    for (const droppedFile of files) {
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((_file: File) => {
          appendFile(_file, droppedFile);
        });
      } else {
        fileCount--;
        if (processedCount === fileCount && fileCount > 0) {
          this.uploadData(fileData, "Seçilen dosyaları yüklemek istediğinize emin misiniz?");
        } else if (fileCount === 0) {

        }
      }
    }
  }

  @HostListener('document:paste', ['$event'])
  async onPaste(event: ClipboardEvent) {
    if (!this.options?.controller || !this.options?.action) {
      return;
    }

    const clipboardData = event.clipboardData;
    if (!clipboardData) { return; }

    const items = clipboardData.items;
    let imageFile: File | null = null;

    for (let i = 0; i < items.length; i++) {
      const item = items[i];
      if (item.kind === 'file' && item.type.startsWith('image/')) {
        imageFile = item.getAsFile();
        if (imageFile) {
          event.preventDefault();
          break;
        }
      }
    }

    if (imageFile) {
      const fileData: FormData = new FormData();
      fileData.append(imageFile.name || `pasted_image_${Date.now()}.png`, imageFile);
      this.uploadData(fileData, "Yapıştırılan resmi yüklemek istediğinize emin misiniz?");
    }
  }

  private uploadData(formData: FormData, confirmationMessage: string) {
    if (confirm(confirmationMessage) == false) {
      this.files = []; // Onaylanmazsa seçimi temizle
      return;
    }

    this.customEvent.emit();

    this.httpClientService.post<any>({
      controller: this.options.controller,
      action: this.options.action,
      queryString: this.options.queryString,
      headers: new HttpHeaders({ "responseType": "blob" })
    }, formData).subscribe({
      next: data => {
        const message = confirmationMessage.startsWith("Yapıştırılan")
          ? "Yapıştırılan resim başarıyla yüklenmiştir"
          : "Dosyalar başarıyla yüklenmiştir";
        this.toastr.message(message, "Dosya Yükleme", {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });
        this.files = [];
      },
      error: (errorResponse: HttpErrorResponse) => {
        const message = confirmationMessage.startsWith("Yapıştırılan")
          ? "Yapıştırılan resim yüklenirken hata ile karşılaşıldı"
          : "Dosya yüklenirken hata ile karşılaşıldı";
        this.toastr.message(message, "Dosya Yükleme", {
          messageType: ToastrMessageType.Error,
          position: ToastrPosition.TopRight
        });
        console.error("Upload error:", errorResponse);
      }
    });
  }
}

export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
}
