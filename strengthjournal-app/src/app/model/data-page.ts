export interface DataPage<T> {
  perPage: number,
  totalRecords: number,
  currentPage: number,
  data: T[]
}