export interface BaseResponse<T> {
  statusCode: number;
  isSuccess: boolean;
  errorMessages: string[];
  result: T;
}

export interface PagedResult<T> {
  items: T[];
  totalItems: number;
  currentPage: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface PagingFilterBase {
  page: number;
  size: number;
  search?: string;
  sortBy?: string;
  isActive?: boolean;
  sortDirection?: 'asc' | 'desc';
}

export interface PagedResponse<T> extends BaseResponse<PagedResult<T>> {}
