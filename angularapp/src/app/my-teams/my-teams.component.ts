import { Component, OnInit  } from '@angular/core';
import { Client, TeamListDTO, GetGameDTO } from 'src/app/Client';
import { Router} from '@angular/router';
import { environment } from '../../environments/environment';

@Component({
    selector: 'app-my-teams',
    templateUrl: './my-teams.component.html'
})
export class MyTeamsComponent implements OnInit{
    teams: TeamListDTO[] = [];
    games: GetGameDTO[] = [];
    private client: Client;
    ngOnInit(): void {
        this.client.teamsAll().subscribe(res => {
            this.teams = res;
        })
        this.client.games().subscribe(res => {
            this.games = res;
        })
    }
    constructor(client: Client, private router: Router) {
        this.client = client;
    }
}