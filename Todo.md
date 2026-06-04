Here's a tight 7-day plan. Scope is "30-second Tron microgame," solo, that you can show on Day 7. Leaderboard is demoted to a 30-min local-only add-on. Power-ups and multiplayer are explicitly **post-jam** and not in the plan.

**North star rule for the player:** Don't be standing on a red tile when it falls, and don't cross your own trail. That's the whole game.

## Day 1 — Project & grid

- [X] Create Unity 6 URP project, name it, save scene as `Main`
- [X] Set up `Assets/Scenes/`, `Scripts/`, `Prefabs/`, `Materials/`, `Audio/`
- [X] `GridManager` script: 2D array of `Cell { Safe, Warning, Falling, Gone }`, configurable size 20×20, cell size 1
- [X] Render grid at runtime with InstancedMeshRenderer (one mesh, GPU instances — perf matters)
- [X] Cell colors: Safe = dark grid line, Warning = red, Falling = flashing, Gone = invisible
- [X] Editor gizmo to draw grid bounds in Scene view
- [X] Smoke test: confirm grid shows, colors switch on a key press

## Day 2 — The 30s fall

- [X] Curve-driven warning times: at 30s mark outer ring Warning, at 20s mark ring 2, at 10s mark ring 3, at 0s mark center
- [X] Warning → 0.5s shake → Falling (red flash + scale) → Gone (despawn + particle)
- [X] 30s countdown timer in the corner of the HUD
- [X] On timer = 5, trigger Win
- [X] Vibe check: is the timing tense? Tune shake/fall duration until it feels right

## Day 3 — Player + trail

- [X] Input System: WASD/arrow keys for 4-dir movement, snap to grid
- [X] Player is a glowing cube with a TrailRenderer behind it
- [X] Trail grows as you move; older trail fades but stays solid for collision
- [X] On movement, mark your current cell as "trail"
- [X] Death checks (run every frame, early-out is fine for 20×20):
  - Current cell is `Gone` → Death
  - Current cell is already `trail` from a previous lap → Death
- [~] Death = slow-mo 0.3s, red flash, freeze, game over screen
- [X] **Day 3 milestone:** you can play and lose on purpose. If you can do that, the rest is polish.

## Day 4 — Score, win/lose, restart

- [ ] Score = tiles traversed + (survival bonus if you reach var seconds timer alive = 1000)
- [ ] HUD: top-left timer, top-right score, both readable against the neon
- [ ] Game states enum: `Menu, Playing, GameOver, Win`
- [ ] `GameManager` controls state, restart reloads scene or resets grid
- [ ] Main menu: title, "PLAY", "QUIT" — placeholder art is fine
- [ ] Game Over screen: "YOU FELL" / "TRAIL HIT" (one of two, tells player why), final score, "AGAIN" button
- [ ] Win screen: "SURVIVED" with score, "AGAIN" button

## Day 5 — Feel pass (this is where the game becomes good) [Cut Due to time]
- [ ] Pause menu (Esc) — required for the "1-minute understandability" rule

## Day 6 — Audio + leaderboard

- [ ] Free synthwave loop (or a single oscillator in code) for BG music, low volume, loops
- [ ] SFX: move tick, trail spawn, tile warning (low pulse), tile fall (boom), death (whoosh)
- [ ] **Local leaderboard, 30 minutes max:**
  - On Win/GameOver, if score > lowest of top 5, show name-entry (3 letters, tron-style)
  - Store as JSON in `Application.persistentDataPath`
  - Leaderboard scene/screen with top 5
- [ ] Settings: volume slider, fullscreen toggle

## Day 7 — Ship

- [ ] Build target decided (PC build for class, probably)
- [ ] Window title, icon (placeholder Unity logo is fine)
- [ ] Title screen art (one neon title text, free font like Orbitron)
- [ ] Play 10 runs back-to-back, fix the worst jank
- [ ] README.md in repo with controls + the one rule
- [ ] Build, copy to a known folder, test on a clean machine if possible

## What to cut first if you fall behind

1. **Day 6 leaderboard** → ship without it, add a "Best: XXX" line in HUD using `PlayerPrefs` (5 min)
2. **Pause menu** → skip, just don't pause
3. **Win screen** → just say "SURVIVED" over the running game with restart button
4. **Camera shake / starfield** → leave defaults
5. **Name entry** → just save score, no name

## What to add only if you're ahead

- Power-ups (1 power-up = slow-fall, that's it)
- A second, harder grid pattern
- A second player on shared keyboard (local hot-seat, ~2 hours)

---

Want me to:
- Write the **GridManager** or **FallScheduler** script to get you started?
- Set up the **Unity `.gitignore` + `.gitattributes`** so you can push Day 1's work tonight?