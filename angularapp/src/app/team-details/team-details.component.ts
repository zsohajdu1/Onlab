import { Component, OnInit } from '@angular/core';
import { Client, MemberApplicationDTO, TeamDetailDTO, TeamStatus } from '../client';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-team-details',
    templateUrl: './team-details.component.html',
})
export class TeamDetailsComponent implements OnInit {
    id: string = "";
    team: TeamDetailDTO;
    isCaptain: boolean = false;
    isPlayer: boolean = false;
    userId: string;
    newName: string;
    captainName: string;
    newStatus: TeamStatus;
    viewStatus: string;
    applications: MemberApplicationDTO[] = [];
    constructor(private activatedRoute: ActivatedRoute, private client: Client) { }
    ngOnInit(): void {
        this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.loadData();
    }

    loadData(): void {
        this.client.teams(parseInt(this.id)).subscribe(res => {
            this.team = res;
            this.team.gameIcon = "https://localhost:7010/" + this.team.gameIcon;
            this.newStatus = this.team.status!;
            if (this.newStatus == 0) this.viewStatus = "LFT";
            if (this.newStatus == 1) this.viewStatus = "FULL";
            this.newName = this.team.name!;
            this.client.user2(this.team.captainId!).subscribe(res => {
                this.captainName = res;
            })
            this.client.user().subscribe(res => {
                this.userId = res;

                if (this.team.captainId == this.userId) {
                    this.isCaptain = true;
                    this.isPlayer = true;
                }
                else if (this.team.membersId?.includes(this.userId)) {
                    this.isPlayer = true;
                }
            })
            this.client.applications(this.team.id!).subscribe(res => {
                this.applications = res;
            })
        })
    }

    changeStatus(status: number): void {
        if (status == 0) {
            this.newStatus = 0;
            this.viewStatus = "LFT";
        }
        if (status == 1) {
            this.newStatus = 1;
            this.viewStatus = "FULL";
        }
    }

    save(): void {
        if (this.newName) {
            this.team.name = this.newName;
            this.team.status = this.newStatus;
            this.client.change(this.team).subscribe(res => {
                this.loadData();
            })
        }
    }

    apply(): void {
        this.client.apply(this.team.id!).subscribe(res => {
        })
    }

    accept(applicationId: number | undefined) {
        this.client.acceptPATCH(this.team.id!, applicationId!).subscribe(res => {
            this.loadData();
        })
    }

    deny(applicationId: number | undefined) {
        this.client.denyPATCH(this.team.id!, applicationId!).subscribe(res => {
            this.loadData();
        })
    }

    kick(playerId: string | undefined): void {
        this.client.remove(playerId!, this.team.id!).subscribe(res => {
            this.loadData();
        })
    }

    leave(): void {
        this.client.remove(this.userId, this.team.id!).subscribe(res => {
            this.loadData();
        })
    }
}
