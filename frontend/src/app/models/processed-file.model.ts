export interface ProcessedFile {
  id: string;
  fileName: string;
  status: string;
  filePath: string;
  processedAt: Date;
  acquirerName?: string;
  establishmentCode?: string;
  processingDate?: Date;
  periodStartDate?: Date;
  periodEndDate?: Date;
  sequenceNumber?: number;
  errorMessage?: string;
}
