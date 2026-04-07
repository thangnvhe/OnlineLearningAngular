import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse, PagedResponse, PagingFilterBase } from '../models/base/base.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export abstract class BaseApiService {
  protected http = inject(HttpClient);
  protected baseUrl = environment.apiUrl;

  // Lấy danh sách không phân trang
  getAll<T>(url: string, params?: any): Observable<BaseResponse<T>> {
    return this.http.get<BaseResponse<T>>(`${this.baseUrl}/${url}`, { params });
  }

  // Lấy danh sách có phân trang
  getAllWithPagination<T>(url: string, params: PagingFilterBase): Observable<PagedResponse<T>> {
    let queryParams = new HttpParams();
    Object.entries(params).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        queryParams = queryParams.append(key, value.toString());
      }
    });

    return this.http.get<PagedResponse<T>>(`${this.baseUrl}/${url}`, { params: queryParams });
  }

  getById<T>(url: string, id: string | number): Observable<BaseResponse<T>> {
    return this.http.get<BaseResponse<T>>(`${this.baseUrl}/${url}/${id}`);
  }

  create<TResponse, TRequest>(
    url: string,
    data: TRequest | FormData,
  ): Observable<BaseResponse<TResponse>> {
    return this.http.post<BaseResponse<TResponse>>(`${this.baseUrl}/${url}`, data);
  }

  update<TResponse, TRequest>(
    url: string,
    id: string | number,
    data: TRequest | FormData,
  ): Observable<BaseResponse<TResponse>> {
    return this.http.put<BaseResponse<TResponse>>(`${this.baseUrl}/${url}/${id}`, data);
  }

  delete<T>(url: string, id: string | number): Observable<BaseResponse<T>> {
    return this.http.delete<BaseResponse<T>>(`${this.baseUrl}/${url}/${id}`);
  }
}
