import { Component } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AuthorizeService } from '../api-authorization/authorize.service';
import { Client, GetGameDTO } from '../Client';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent {
    
    public isAuthenticated?: Observable<boolean>;
    public userName?: Observable<string | null | undefined>;
    games: GetGameDTO[] = [];    

    constructor(private authorizeService: AuthorizeService, private client: Client) {
        client.games().subscribe( res =>
            this.games = res
            );
     }

    ngOnInit() {
        this.isAuthenticated = this.authorizeService.isAuthenticated();
        this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    }
}
