import { Component, DoCheck, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Client, GetGameDTO, TournamentStatus, TournamentListDTO } from 'src/app/client';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Component({
    selector: 'app-my-tournaments',
    templateUrl: './my-tournaments.component.html'
})
export class MyTournamentsComponent implements OnInit, DoCheck {
    tournaments: TournamentListDTO[] = [];
    games: GetGameDTO[] = [];
    private client: Client;
    status: TournamentStatus;
    oldStatus: TournamentStatus;
    name: string;
    oldName: string;
    gameId: number;
    oldgameId: number;

    viewStatus: string = "Select Status";
    viewGame: string = "Select Game";

    ngOnInit(): void {
        this.updateTournaments(undefined, undefined, undefined);
        this.client.games().subscribe(res => {
            this.games = res;
        })
    }
    constructor(client: Client, private router: Router) {
        this.client = client;
    }

    updateTournaments(name: string | undefined, status: TournamentStatus | undefined, gameId: number | undefined): void {
        this.client.my2(name, status, gameId).subscribe(res => {
            this.tournaments = res;
            this.tournaments.forEach(t => t.gameIcon = "https://localhost:7010/" + t.gameIcon)
        })
    }

    ngDoCheck(): void {
        if (this.status != this.oldStatus) {
            this.oldStatus = this.status;
            this.updateTournaments(this.name, this.status, this.gameId);
        }
        if (this.gameId != this.oldgameId) {
            this.oldgameId = this.gameId;
            this.updateTournaments(this.name, this.status, this.gameId);
        }
        if (this.name != this.oldName) {
            this.oldName = this.name;
            this.updateTournaments(this.name, this.status, this.gameId);
        }
    }

    changeGameId(game: GetGameDTO | undefined): void {
        if (game && game.id) {
            this.gameId = parseInt(game.id);
            this.viewGame = game.name!;
        }
    }
    changeStatus(status: number | undefined): void {
        if (status != undefined) {
            this.status = status;
            if (status == 0) this.viewStatus = "Upcoming";
            else if (status == 1) this.viewStatus = "In Progress";
            else if (status == 2) this.viewStatus = "Completed";
        }
    }
}