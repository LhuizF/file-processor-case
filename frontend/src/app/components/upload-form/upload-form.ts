import { Component, EventEmitter, Output } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-upload-form',
  templateUrl: './upload-form.html',
  styleUrls: ['./upload-form.scss'],
  imports: [CommonModule]
})
export class UploadForm {

  @Output() uploadSuccess = new EventEmitter<void>();

  selectedFile: File | null = null;
  isLoading = false;
  uploadMessage = '';
  isError = false;

  constructor(private apiService: ApiService) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      this.uploadMessage = '';
      this.isError = false;
    }
  }

  onUpload(): void {
    if (!this.selectedFile) {
      this.uploadMessage = 'Por favor, selecione um arquivo antes de enviar.';
      this.isError = true;
      return;
    }

    this.isLoading = true;
    this.isError = false;
    this.uploadMessage = 'Enviando arquivo...';

    this.apiService.uploadFile(this.selectedFile).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.uploadMessage = 'Arquivo enviado com sucesso e enfileirado para processamento!';
        this.selectedFile = null;
        this.uploadSuccess.emit()
      },
      error: (error: HttpErrorResponse) => {
        this.isLoading = false;
        this.isError = true;
        this.uploadMessage = `Erro ao enviar o arquivo: ${error.error?.message || error.message}`;
      },
      complete: () => {
        setTimeout(() => {
          this.uploadMessage = '';
        }, 3000);
      }
    });
  }
}
