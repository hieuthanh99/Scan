import {Injectable} from "@angular/core";
import {BehaviorSubject, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthIframeService {
  tokenSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  token: Observable<boolean> = this.tokenSubject.asObservable();
}
