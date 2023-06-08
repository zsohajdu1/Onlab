import { Component, OnInit  } from '@angular/core';
import { Client, TeamListDTO, GetGameDTO, TeamStatus } from 'src/app/client';
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
    status: TeamStatus;
    oldStatus: TeamStatus;
    name: string;
    oldName: string;
    gameId: number;
    oldgameId: number;

    viewStatus: string = "Select Status";
    viewGame: string = "Select Game";
    ngOnInit(): void {
        this.updateTeams(undefined, undefined, undefined);
        this.client.games().subscribe(res => {
            this.games = res;
            this.games.sort((a, b) => a!.tournamentCount! - b.tournamentCount!); 
        })
    }
    constructor(client: Client, private router: Router) {
        this.client = client;
    }

    updateTeams(name: string | undefined, status: TeamStatus | undefined, gameId: number | undefined): void {
        this.client.my(name, status, gameId).subscribe(res => {
            this.teams = res;
            this.teams.forEach(t => t.gameIcon = "https://localhost:7010/" + t.gameIcon)
        })
    }

    ngDoCheck(): void {
        if (this.status != this.oldStatus) {
            this.oldStatus = this.status;
            this.updateTeams(this.name, this.status, this.gameId);
        }
        if (this.gameId != this.oldgameId) {
            this.oldgameId = this.gameId;
            this.updateTeams(this.name, this.status, this.gameId);
        }
        if (this.name != this.oldName) {
            this.oldName = this.name;
            this.updateTeams(this.name, this.status, this.gameId);
        }
    }

    changeGameId(game: GetGameDTO | undefined): void {
        if (game?.id) {
            this.gameId = parseInt(game.id);
            this.viewGame = game.name!;
        }
    }
    changeStatus(status: number | undefined): void {
        if (status != undefined) {
            this.status = status;
            if (status == 0) this.viewStatus = "LFT";
            else this.viewStatus = "FULL";
        }
    }
}