**미디어 시제품 경진대회(대상), 파란학기제(이노베이터상) 수상, GEEKS, GGC(글로벌 게임 챌린지) 2024에 출품**한 "VR 역사테마 액션 X 방탈출 게임 프로젝트 - PEACETOL"입니다.

→ 독립운동가, 김상옥 의사님의 생애를 체험해볼 수 있는 VR 역사 게임 프로젝트입니다.

---

Youtube: [Trailer Video](https://youtu.be/h1Rpf5mIIoI) 

Build File: [Last Battle(for demonstrate)](https://drive.google.com/file/d/1dfoa6KEexU5XEDARfGKGQ9G0Wl4EAg0j/view?usp=sharing)

Ref Video: [경성피스톨](https://www.youtube.com/watch?v=cJLAUB5NHE4&t=936s), [독립운동가 김상옥](https://www.youtube.com/watch?v=IS5VD5Aa6AQ)

<br>

### 1. 프로젝트 소개

- Unity Engine을 기반으로 제작된 VR 역사테마 액션 X 방탈출 게임 제작 프로젝트입니다.

<br>

### 2. 프로젝트 개요

- 제작 기간: 2.5개월
- 개발 도구: Unity 2022.3.39f1
- 개발 언어: C#

- **ACT Scene(JONGNO, FINAL) 프로젝트 클래스 다이어그램**
<div align="center">
<img src="https://github.com/user-attachments/assets/81b84a89-5e9e-430b-81c3-809eb53e3c65" alt="image">
</div>

<br>


### 3. 기술

- Unity
>- XR Interaction Toolkit
>- Unity VR
 
- VR
>- Occulus Quest 2, 3

- Git
>- Terminal-GitBash를 활용했고, Branch는 Git-Flow 형식으로 관리하였습니다.
>- VS Code의 Git Graph Extension을 활용하였습니다.

- Modeling
>- Reallusion
>- Blender

+) Mixamo

<br>


### 4. 플레이 설명

- 조작법
>- 이동: Right Thumbstick
>- 회전: Left Thumbstick
>- 잡기: Right/Left Hand Trigger(Grip)
>- 텔레포트: Left Hand Trigger
>- 사격: `잡기` 후, Right/Left Index Trigger

<br>

<div align="center">
<img src="https://github.com/user-attachments/assets/b1159a07-5362-4500-bdad-9b2ee4621e72" alt="image">
</div>

<br>

### 5. 구현 내용
- 게임 흐름
> 지문 설명: 게임 상황에 대한 이해와 이후 진행해야 할 미션을 파악하기 위한 지문입니다.

<div align="center">
<img src="https://github.com/user-attachments/assets/36f2d243-a638-4a31-b1c8-320dbe333144" alt="image">
</div>

<br>

> GameFlow Light: 게임 흐름을 자연스럽게 따라가도록 하기 위한 빛이 존재합니다.

<div align="center">
<img src="https://github.com/user-attachments/assets/faa96ef1-ea60-4164-afd1-f08d47f043ae" alt="image">
</div>

<br>

> NPC 스크립트, 플레이어 독백을 통해서도 게임 흐름에 대한 설명이 진행됩니다.

 <div align="center">
    <img src="https://github.com/user-attachments/assets/540262bd-8eec-446d-a96d-36533e663975" alt="image" width="1000">
</div>

<br>

- **종로경찰서: 적의 눈을 피해 경찰서로 이동 후 폭탄을 투척해야 합니다. 자연스럽게 행동하면 적의 눈을 피할 수 있습니다.**
>- 적 FSM 1: Patrolling Enemy, Standing Enemy
>- 폭탄
>- Safe Areas: 포스터 근처, 시장 상인 근처
  
 <div align="center">
    <img src="https://github.com/user-attachments/assets/993978b3-c9ea-4416-96ab-52f3d6131765" alt="image" width="900">
</div>


 <br>
 
- **마지막 전투(시가전): 플레이어가 총을 쏘거나 이동할 때만 시간이 정상적으로 흐르며, 가만히 있을 경우 시간이 천천히 흐릅니다. 이를 이용해 스타일리쉬한 전투가 가능합니다(ref: [SUPERHOT](https://store.steampowered.com/app/322500/SUPERHOT/?l=koreana)). 적을 처치해 총을 뺏을 수 있고, 지붕을 넘어다니며 최종 구역까지 이동할 수 있습니다. 적은 끊임없이 몰려옵니다.**
>- 적 FSM 2: One Hand Enemy, Two Hand Enemy
>- 피스톨(한손), 소총(두손)

 <div align="center">
    <img src="https://github.com/user-attachments/assets/445965c3-36ab-46f2-90af-cb88a71b59d1" alt="image" width="900">
</div>

<br>

>- Teleport Areas
  
 <div align="center">
    <img src="https://github.com/user-attachments/assets/b157b996-f1ac-45d0-b22b-380995e2ce8a" alt="image" width="380">
</div>

 <br>
 
>- 총 휴대(socket)
<div align="center">
    <img src="https://github.com/user-attachments/assets/4d8bb097-b07a-440e-973f-a0859306dfb3" alt="image" width="600">
</div>

 <br>
 
>- **적, 총알, 총(drop) 관리 : Object Pooling을 통해 관리됩니다.**

<br>

- 안개와 빛 효과
<div align="center">
    <img src="https://github.com/user-attachments/assets/f7fc2b50-3404-4fbf-86aa-6cfb56b5abe1" alt="image" width="600">
</div>

<br>

+) 단순화한 적 FSM 구조(액션 게임)
- **종로경찰서 씬** : 한 적이 플레이어를 감지하기 시작하면 모든 적의 상태가 Chase로 변경됩니다.
>- Patrol Enemy: Patrol -> Chase <-> Attack
>- Idle Enemy: Idle -> Chase <-> Attack

<br>


- **마지막 전투 씬**
<div align="center">
    <img src="https://github.com/user-attachments/assets/e596d9f5-ff0c-4e9c-a8c5-aee209a26ecd" alt="image" width="400">
</div>

<br>

+) 추가 방탈출 로직: https://github.com/sonnypyo

<br>

### 6. 추가 이미지
- In game

<div align="center">
    <img src="https://github.com/user-attachments/assets/8699fdf6-5587-47e5-b424-a246498888a6" alt="image" width="1000">
</div>

<div align="center">
    <img src="https://github.com/user-attachments/assets/d2cc7c96-85d6-473d-88ed-50ac268b0d76" alt="image" width="1000">
</div>

<div align="center">
    <img src="https://github.com/user-attachments/assets/5819eec5-8112-49d3-9f00-b2d895ef63ac" alt="image" width="700">
</div>


<br>

- Character Voice
<div align="center">
    <img src="https://github.com/user-attachments/assets/f177fdbd-b3e5-4e3b-a33d-d27a4d6e3cef" alt="image" width="340">
</div>


<br>

- GGC, GEEKS 참여 부스

<div align="center">
    <img src="https://github.com/user-attachments/assets/46753d10-aa87-4759-aefb-082b718ccf8e" alt="image" width="600">
</div>


<br>

- Showcase Poster
<div align="center">
    <img src="https://github.com/user-attachments/assets/7c04aff7-0775-4cfe-989b-f8a3089d6a2f" alt="image" width="700">
</div>


### 7. 팀 정보

>프로젝트: PEACETOL
>
>팀명: Immersive Real Time (IRT)

<br> <table align="center" width="900"> <thead> <tr> <th width="130" align="center">성명</th> <th width="270" align="center">소속</th> <th width="300" align="center">역할</th> <th width="100" align="center">깃허브</th> <th width="180" align="center">이메일</th> </tr> </thead> <tbody> <tr> <td width="130" align="center">손준표<br/>(팀장)</td> <td width="270" align="center">Ajou Univ - Digital Media</td> <td width="300">VR 방탈출 게임 메인 기획/개발 </br></td> <td width="100" align="center"> <a href="https://github.com/sonnypyo"> <img src="http://img.shields.io/badge/sonnypyo-655ced?style=social&logo=github"/> </a> </td> <td width="175" align="center"> <a href="mailto:jp1598@ajou.ac.kr"> <img src="https://img.shields.io/badge/jp1598-655ced?style=social&logo=gmail"/> </a> </td> </tr> <tr> <td width="130" align="center">손현진</td> <td width="270" align="center">Ajou Univ - Digital Media</td> <td width="300">VR 액션 게임 메인 기획/개발</br> </td> <td width="100" align="center"> <a href="https://github.com/sonyrainy"> <img src="http://img.shields.io/badge/sonyrainy-655ced?style=social&logo=github"/> </a> </td> <td width="175" align="center"> <a href="mailto:thsguswls610@gmail.com"> <img src="https://img.shields.io/badge/thsguswls610-655ced?style=social&logo=gmail"/> </a> </td> </tr> <tr> <td width="130" align="center">주재석</td> <td width="270" align="center">Ajou Univ - Digital Media</td> <td width="300">3D 모델링</br> 캐릭터 애니메이션</td> <td width="100" align="center">-</td> <td width="175" align="center"> <a href="mailto:jujae77@ajou.ac.kr"> <img src="https://img.shields.io/badge/jujae77-655ced?style=social&logo=gmail"/> </a> </td> </tr> </tbody> </table>



<br>

 > Supporter

<br>  
<table align="center" width="900">  
<thead>  
<tr>  
<th width="130" align="center">성명</th>  
<th width="270" align="center">소속</th>  
<th width="300" align="center">역할</th>  
<th width="180" align="center">이메일</th>  
</tr>  
</thead>  
<tbody>  
<tr>  
<td align="center">석설호</td>  
<td align="center">Ajou Univ - Digital Media</td>  
<td align="center">트레일러 영상 제작</td>  
<td align="center">  
   <a href="mailto:suk0104@ajou.ac.kr">  
      <img src="https://img.shields.io/badge/suk0104-655ced?style=social&logo=gmail"/>  
   </a>  
</td>  
</tr>  
<tr>  
<td align="center">고명진</td>  
<td align="center">Ajou Univ - Digital Media</td>  
<td align="center">캐릭터 보이스</td>  
<td align="center">  
   <a href="mailto:audwls5421@ajou.ac.kr">  
      <img src="https://img.shields.io/badge/audwls5421-655ced?style=social&logo=gmail"/>  
   </a>  
</td>  
</tr>  
</tbody>  
</table>  

