export interface StatsModel {
  totalFiles: number;
  success: {
    total: number;
    percent: number;
  };
  failure: {
    total: number;
    percent: number;
  };
}
