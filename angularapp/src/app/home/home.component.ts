import { Component } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AuthorizeService } from '../api-authorization/authorize.service';
import { Client, GetGameDTO, TeamListDTO } from '../client';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent {

    public isAuthenticated?: Observable<boolean>;
    public userName?: Observable<string | null | undefined>;
    games: GetGameDTO[] = [];
    teams: TeamListDTO[] = [];

    constructor(private authorizeService: AuthorizeService, private client: Client) {
        client.games().subscribe(res => {
            this.games = res;
            this.games.forEach(g => g.icon = "https://localhost:7010/" + g.icon)
        }
        );
        client.my(undefined, undefined, undefined).subscribe(res => {
            this.teams = res;
        })
    }

    ngOnInit() {
        this.isAuthenticated = this.authorizeService.isAuthenticated();
        this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    }
}
