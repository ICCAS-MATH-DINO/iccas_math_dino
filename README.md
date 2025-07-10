# Unity Quiz Running Game

이 프로젝트는 Unity로 개발된 3D 퀴즈 러닝 게임입니다. 플레이어가 앞으로 달리면서 퀴즈를 풀어나가는 방식으로, 정답을 맞추면 시간이 추가되고 틀리면 시간이 감소하는 시간 제한 방식의 게임입니다.

## 주요 기능

- **자동 달리기**: 플레이어가 자동으로 앞으로 달립니다.
- **퀴즈 시스템**: 
  - 플레이어 앞에 두 개의 선택지가 나타납니다
  - 정답 선택 시 +10초
  - 오답 선택 시 -5초 및 진동
  - 시간 제한 있음 (기본 10초)
- **다국어 지원**: 한국어, 영어, 독일어 지원
- **배경음악**: 메뉴와 게임 플레이에서 각각 다른 BGM 재생
- **일시정지 기능**: ESC 키로 게임 일시정지 가능

## 프로젝트 구조

### 핵심 매니저 클래스
- `GameManager.cs`: 전체 게임 상태 관리
- `TimeManager.cs`: 게임 시간 관리 (싱글톤)
- `QuizManager.cs`: 퀴즈 생성 및 관리
- `BGMManager.cs`: 배경음악 관리 (싱글톤)
- `MainMenuManager.cs`: 메인 메뉴 UI 관리
- `PauseManager.cs`: 일시정지 기능 관리

### 플레이어 관련
- `PlayerController.cs`: 플레이어 이동 및 애니메이션 제어
- `CameraFollow.cs`: 플레이어 추적 카메라

### 퀴즈 시스템
- `QuizLoader.cs`: 퀴즈 데이터 파일 로드
- `QuizData.cs`: 퀴즈 데이터 구조 정의
- `QuizCollisionHandler.cs`: 퀴즈 선택지 충돌 처리
- `QuizOptionCollider.cs`: 선택지 충돌 감지

## 퀴즈 데이터 형식
퀴즈 데이터는 `StreamingAssets` 폴더의 텍스트 파일에서 관리됩니다.
```
질문,선택지1,선택지2,정답번호
```

예시:
```
1+1은?,2,3,1
대한민국의 수도는?,서울,부산,1
```

## 개발 환경
- Unity 6000.1.9f1
- 개발 언어: C#

## 빌드 플랫폼
- PC (Windows/Mac)
- 모바일 (Android/iOS) 
