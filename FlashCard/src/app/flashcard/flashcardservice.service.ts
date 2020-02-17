import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Question } from 'src/Interfaces/Question';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlashcardserviceService {

  constructor(private http: HttpClient) {}

  baseUrl: string = 'https://flashCard_api:5001';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };

  getAllFlashCards$(): Observable<Question[]> {
      return this.http.get<Question[]>(`${this.baseUrl}/api/flashcard/getall`)
  }

  getFlashCard$(id: number): Observable<Question> {
    return this.http.get<Question>(`${this.baseUrl}/api/flashcard/get/${id}`)
  }

  postFlashcard$(q: Question): Observable<Question> {
    return this.http.post<Question>(`${this.baseUrl}/api/flashcard/post`, q, this.httpOptions)
  }

  updateFlashcard$(q: Question): Observable<Question> {
    return this.http.put<Question>(`${this.baseUrl}/api/flashcard/update`, q, this.httpOptions)
  }


}
