using jp.illusive_isc.ReFrame.Core;
using UnityEngine;

namespace jp.illusive_isc.ReFrame.Plium.Chiyu
{
    /// <summary>ちゆ (Chiyu) 用の ReFrame 宣言。</summary>
    [AddComponentMenu("ILLUSORY OVERRIDE/ReFrame/ChiyuReFrame")]
    [ReFrameAvatarSignature(
        "ちゆ",
        FbxGuids = new[] { "159421e28bd38564d81481b56dbdaf9e" },
        AvatarNames = new[] { "ChiyuAvatar" }
    )]
    [ReFrameTheme("Packages/jp.illusive-isc.reframe-chiyu/Editor/UI/ChiyuTheme.uss")]
    // Quest: 法線マップからの陰の焼き込みは切る (ちゆの髪・服の法線マップは凹凸が強く、髪に黒い筋・カーディガンに黒い縞が出た)。
    // 明るさは全体 0.83 で肌・服が PC と同じ (白い方向光 1 + 環境光 0.2 で lilToon と比べて 0.96〜1.05)。
    // 髪とソックスだけ Quest の方が 8〜11% 明るく黄色寄り (PC はマットキャップ・リムで銀がかる) なので個別に下げて青へ寄せる。
    [ReFrameQuestBake(Brightness = 0.83f, ShadowFromNormalMap = false, MaxTextureSize = 1024)]
    [ReFrameQuestBrightness("FrontHair", Brightness = 0.68f)]
    [ReFrameQuestBrightness("BackHair", Brightness = 0.68f)]
    [ReFrameQuestBrightness("HairBase", Brightness = 0.68f)]
    [ReFrameQuestTint("FrontHair", R = 0.99f, G = 1f, B = 1.05f)]
    [ReFrameQuestTint("BackHair", R = 0.99f, G = 1f, B = 1.05f)]
    [ReFrameQuestTint("HairBase", R = 0.99f, G = 1f, B = 1.05f)]
    [ReFrameQuestBrightness("Chiyu_Cloth/Socks", Brightness = 0.66f)]
    [ReFrameQuestTint("Chiyu_Cloth/Socks", R = 1f, G = 1f, B = 1.06f)]
    // 顔の重ね描きの枠 (Face_Transparent、頬染め・コミカル表現など) は Quest では不透明に描かれ、頬染めが顔の真ん中の帯になる → 透明な三角形を切る。
    [ReFrameQuestCutTransparent("Body", 2)]
    // Quest の揺れ物は 8 本・ボーン 64 まで。前髪・ツインテール・尻尾・胸・耳の 8 本でボーン 61・当たり判定 0 に収まる
    // (後ろ髪 61・スカート 66 は 1 本で上限近いので止める)。Quest 簡易対応版を作ったときの初期値。
    [ReFrameQuestKeepPhysBones("FrontHair_Root", "Twintail_L", "Twintail_R", "Tail", "Breast_L", "Breast_R", "Ear_L", "Ear_R")]
    [ReFramePhysBoneGroup("前髪", "FrontHair_Root")]
    [ReFramePhysBoneGroup("横髪", "SideHair")]
    [ReFramePhysBoneGroup("ツインテール", "Twintail")]
    [ReFramePhysBoneGroup("後ろ髪", "BackHair")]
    [ReFramePhysBoneGroup("耳", "Ear_L", "Ear_R")]
    [ReFramePhysBoneGroup("胸", "Breast")]
    [ReFramePhysBoneGroup("お尻", "Butt")]
    [ReFramePhysBoneGroup("尻尾", "Tail")]
    [ReFramePhysBoneGroup("スカート", "Skirt_Root")]
    [ReFramePhysBoneGroup("カーディガン", "Cardigan")]
    [ReFramePhysBoneGroup("リボン・襟", "ChestRibbon", "HeadRibbon", "SailorCollar")]
    [ReFramePhysBoneGroup("鞄", "SchoolBag", "Bag1")]
    [ReFramePhysBoneGroup("ぬいぐるみ", "POTECHI", "Ear_Root", "Body_Root", "Tail_000")]
    [ReFrameGroupOrder("Closet", "Cloth", "Hair", "Gimmick", "Face", "Props", "JumpAndDash", "Locomotion", "VRCEmote")]
    [ReFrameGroupLabel("Closet", "衣装・髪")]
    [ReFrameGroupLabel("Cloth", "服")]
    [ReFrameGroupLabel("Hair", "髪")]
    [ReFrameGroupLabel("Gimmick", "ギミック")]
    [ReFrameGroupLabel("Face", "表情")]
    [ReFrameGroupLabel("Props", "小物")]
    [ReFrameGroupLabel("JumpAndDash", "ジャンプ・ダッシュ")]
    [ReFrameGroupLabel("Locomotion", "ロコモーション")]
    [ReFrameGroupLabel("VRCEmote", "エモート")]
    public class ChiyuReFrame : ReFrameDeleteComponent
    {
        // ---- 服 (Chiyu_Cloth の MA メニュー。1 = 着たまま固定してメニューだけ消す、0 = 脱いで固定) ----

        [ReFrameDelete("Chiyu_Cardigan", ReFrameParameterType.Bool)]
        [ReFrameLabel("カーディガン")]
        public ReFrameDeleteEntry cardigan = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Onepiece", ReFrameParameterType.Bool)]
        [ReFrameLabel("ワンピース")]
        // 着たまま固定 (1) なら、Chiyu_Shrink2 (ワンピースの下で潰して隠す、面積 0.38 倍) で動く体の三角形を切る。
        [ReFrameCutByBlendShape("body_b", "Chiyu_Shrink2")]
        public ReFrameDeleteEntry onepiece = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Socks", ReFrameParameterType.Bool)]
        [ReFrameLabel("ソックス")]
        // 着たまま固定 (1) なら、Chiyu_Shrink1 (ソックスの下で潰して隠す、面積 0.1 倍) で動く体の三角形を切る。
        [ReFrameCutByBlendShape("body_b", "Chiyu_Shrink1")]
        public ReFrameDeleteEntry socks = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Loafers", ReFrameParameterType.Bool)]
        [ReFrameLabel("ローファー")]
        public ReFrameDeleteEntry loafers = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Beret", ReFrameParameterType.Bool)]
        [ReFrameLabel("ベレー帽")]
        public ReFrameDeleteEntry beret = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_HeadRibbon", ReFrameParameterType.Bool)]
        [ReFrameLabel("頭のリボン")]
        public ReFrameDeleteEntry headRibbon = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Hairpin", ReFrameParameterType.Bool)]
        [ReFrameLabel("ヘアピン")]
        public ReFrameDeleteEntry hairpin = new() { Enabled = false, Value = 1f };

        // 0 = レギンス、1 = パンツ (レギンスが出ている間はパンツが MA Object Toggle で隠れる)。
        [ReFrameDelete("Chiyu_Leggings_or_Pants", ReFrameParameterType.Bool)]
        [ReFrameValueLabels("レギンス", "パンツ")]
        [ReFrameLabel("レギンス / パンツ")]
        public ReFrameDeleteEntry leggingsOrPants = new() { Enabled = false, Value = 0f };

        [ReFrameMenuGroup("Closet", "Cloth")]
        [ReFrameLabel("下着 (メッシュごと消す)")]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("Bra")]
        [ReFrameDeleteObject("Pants")]
        public ReFrameDeleteEntry underwear = new() { Enabled = false, Value = 0f };

        // ---- 髪 (Hair Menu の MA メニュー) ----

        // 0 にすると耳 (Ear) と尻尾 (Tail) のメッシュが消える。
        [ReFrameDelete("Chiyu_EarTail", ReFrameParameterType.Bool)]
        [ReFrameLabel("耳・尻尾")]
        public ReFrameDeleteEntry earTail = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Twintail", ReFrameParameterType.Bool)]
        [ReFrameLabel("ツインテール")]
        public ReFrameDeleteEntry twintail = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_SideHair", ReFrameParameterType.Bool)]
        [ReFrameLabel("横髪")]
        public ReFrameDeleteEntry sideHair = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Front_Length", ReFrameParameterType.Float)]
        [ReFrameLabel("前髪の長さ")]
        public ReFrameDeleteEntry frontLength = new() { Enabled = false, Value = 0.5f };

        [ReFrameDelete("Chiyu_Front_Type", ReFrameParameterType.Float)]
        [ReFrameLabel("前髪の形")]
        public ReFrameDeleteEntry frontType = new() { Enabled = false, Value = 0f };

        // 髪型を差し替えるとき用 (かぐやと同じ)。髪のメッシュと、後ろ髪の回転 (Hair_Rotation の Contact と FX_Control の枝) を消す。
        [ReFrameMenuGroup("Closet", "Hair")]
        [ReFrameLabel("髪 (髪型を差し替えるとき)")]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("BackHair")]
        [ReFrameDeleteObject("FrontHair")]
        [ReFrameDeleteObject("HairBase")]
        [ReFrameDeleteObject("Chiyu Contact/Hair Rotation Receiver")]
        [ReFrameDeleteObject("Chiyu Contact/Hair Rotation Sender")]
        [ReFrameDeleteRelatedBlendTree("Hair_Rotation")]
        // 髪の骨と揺れ物。髪のメッシュ以外は使っていないが、Hair Menu の層 (耳・尻尾の切り替えと同居) が動かし続けるので掃除では消えない。
        [ReFrameDeleteObject("Armature/Hips/Spine/Chest/Neck/Head/FrontHair_Root")]
        [ReFrameDeleteObject("Armature/Hips/Spine/Chest/Neck/Head/BackHair_Root")]
        [ReFrameDeleteObject("Armature/Hips/Spine/Chest/Neck/Head/SideHair_Root")]
        [ReFrameDeleteObject("Armature/Hips/Spine/Chest/Neck/Head/SideHair_PB")]
        [ReFrameDeleteObject("Armature/Hips/Spine/Chest/Neck/Head/SideHair_PB2")]
        [ReFrameDeleteObject("Armature/Hips/Spine/Chest/Neck/Head/Twintail_L")]
        [ReFrameDeleteObject("Armature/Hips/Spine/Chest/Neck/Head/Twintail_R")]
        public ReFrameDeleteEntry hair = new() { Enabled = false, Value = 0f };

        // ---- 表情 (公式 FX) ----

        [ReFrameDelete("Blink_Off", ReFrameParameterType.Bool)]
        [ReFrameLabel("まばたき OFF")]
        public ReFrameDeleteEntry blinkOff = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Face_Lock", ReFrameParameterType.Bool)]
        [ReFrameValueLocked]
        [ReFrameLabel("表情ロック")]
        public ReFrameDeleteEntry faceLock = new() { Enabled = false, Value = 0f };

        // 0 にすると撫での受け口 (Contact) も消し、撫でられたときの遷移を切る。
        [ReFrameDelete("NadeNade_Toggle", ReFrameParameterType.Bool)]
        [ReFrameDeleteObject("Chiyu Contact/nadenadecontact", 0f)]
        [ReFrameCutUndrivenTransitions("Chiyu_NadeNade", EvaluateAsFixed = true)]
        [ReFrameLabel("なでなで")]
        public ReFrameDeleteEntry nadeNade = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Face_Variation", ReFrameParameterType.Int)]
        [ReFrameValueChoices("通常=0", "かわいい=1", "クール=2", "コミカル=3")]
        [ReFrameLabel("表情差分")]
        public ReFrameDeleteEntry faceVariation = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Chiyu_Face_Mogumogu", ReFrameParameterType.Bool)]
        [ReFrameDelete("Chiyu_Face_Mogumogu_F", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameLabel("もぐもぐ")]
        public ReFrameDeleteEntry mogumogu = new() { Enabled = false, Value = 0f };

        // FaceEmo などで表情を作り直すとき用。Gesture にも同じ名前の層 (手の形) があるので FX の層だけ消す。
        [ReFrameMenuGroup("Gimmick", "Face")]
        [ReFrameLabel("表情プリセット (FaceEmo 用)")]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteLayer("Left_Hand", FxOnly = true)]
        [ReFrameDeleteLayer("Right_Hand", FxOnly = true)]
        public ReFrameDeleteEntry faceGesturePreset = new() { Enabled = false, Value = 0f };

        // 0 (胸小さく 100) / 0.5 (どちらも 0) / 1 (胸大きく 100) の 1D BlendTree。
        [ReFrameDelete("Breast_Size", ReFrameParameterType.Float)]
        [ReFrameApplyToAvatar]
        [ReFrameLabel("胸の大きさ")]
        public ReFrameDeleteEntry breastSize = new() { Enabled = false, Value = 0.5f };

        [ReFrameMenuGroup("Gimmick")]
        [ReFrameApplyToAvatar]
        [ReFrameLabel("足: ヒールオフ")]
        [ReFrameBlendShape("body_b", "heel_OFF_ヒールオフ_R")]
        [ReFrameBlendShape("body_b", "heel_OFF_ヒールオフ_L")]
        public ReFrameDeleteEntry heelOff = new() { Enabled = false, Value = 0f };

        [ReFrameMenuGroup("Gimmick")]
        [ReFrameApplyToAvatar]
        [ReFrameLabel("足: ハイヒール")]
        [ReFrameBlendShape("body_b", "highheel_ハイヒール_R")]
        [ReFrameBlendShape("body_b", "highheel_ハイヒール_L")]
        public ReFrameDeleteEntry highHeel = new() { Enabled = false, Value = 0f };

        // ---- 小物 (Props。それぞれ MA の部品なので、オブジェクトごと消す) ----

        // 鞄から小物を取り出す仕組みごと消す。小物そのものはメニューから出せる。
        // Gimmick_Contact (小物を触って操作する) の宣言は鞄の MA Parameters にあるので、鞄の根元は残す。
        [ReFrameDelete("SchoolBag", ReFrameParameterType.Bool)]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteLayer("SchoolBag")]
        [ReFrameDeleteLayer("SchoolBag_Gimmick")]
        [ReFrameDeleteObject("SchoolBag/Armature")]
        [ReFrameLabel("鞄")]
        public ReFrameDeleteEntry schoolBag = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Gimmick_Contact", ReFrameParameterType.Bool)]
        [ReFrameLabel("小物を触って操作")]
        public ReFrameDeleteEntry gimmickContact = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Glasses", ReFrameParameterType.Bool)]
        [ReFrameDelete("Glasses_Hand", ReFrameParameterType.Bool)]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("Glasses")]
        [ReFrameLabel("眼鏡")]
        public ReFrameDeleteEntry glasses = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Food", ReFrameParameterType.Bool)]
        [ReFrameDelete("Food_Hand", ReFrameParameterType.Bool)]
        [ReFrameDelete("Food_Eat_Types", ReFrameParameterType.Bool)]
        [ReFrameDelete("SchoolBag_Random", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("Food")]
        [ReFrameLabel("食べ物")]
        public ReFrameDeleteEntry food = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Phone", ReFrameParameterType.Bool)]
        [ReFrameDelete("Phone_Light_On", ReFrameParameterType.Bool)]
        [ReFrameDelete("Phone_Clock", ReFrameParameterType.Bool)]
        [ReFrameDelete("Phone_Date", ReFrameParameterType.Bool)]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("Phone")]
        [ReFrameLabel("スマホ")]
        public ReFrameDeleteEntry phone = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("PortableFan", ReFrameParameterType.Bool)]
        [ReFrameDelete("PortableFan_Hand", ReFrameParameterType.Bool)]
        [ReFrameDelete("PortableFan_Low", ReFrameParameterType.Bool)]
        [ReFrameDelete("PortableFan_High", ReFrameParameterType.Bool)]
        [ReFrameDelete("PortableFan_Hand_On", ReFrameParameterType.Bool)]
        [ReFrameDelete("PortableFan_Out", ReFrameParameterType.Bool)]
        [ReFrameDelete("PortableFan_Hand_Rotation_X", ReFrameParameterType.Float)]
        [ReFrameDelete("PortableFan_Hand_Rotation_Y", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("PortableFan")]
        [ReFrameLabel("ハンディファン")]
        // 風は最大 毎秒 10 × 寿命 4 秒 = 40 粒 (FX が rateOverTime を 0 / 2 / 10 に切り替える)。
        [ReFrameSetMaxParticles("PortableFan/Armature/Hips/Spine/Chest/sholder_R/Upperarm_R/Lowerarm_R/Right Hand/PortableFan/Particle System", 50, Always = true)]
        [ReFrameQuestParticleMaterial("PortableFan", Recursive = true)]
        public ReFrameDeleteEntry portableFan = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("POTECHI_nui", ReFrameParameterType.Bool)]
        [ReFrameDelete("POTECHI_VRMode", ReFrameParameterType.Bool)]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("POTECHI_nui")]
        [ReFrameLabel("ぬいぐるみ (抱っこ)")]
        public ReFrameDeleteEntry potechiNui = new() { Enabled = false, Value = 0f };

        // ---- ジャンプ・ダッシュ (JumpAndDash の MA 部品) ----

        [ReFrameDelete("Jump_Collider", ReFrameParameterType.Bool)]
        [ReFrameDelete("Dash_Collider", ReFrameParameterType.Bool)]
        [ReFrameDelete("Collider_On", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("JumpAndDash")]
        [ReFrameLabel("ジャンプ・ダッシュ")]
        [ReFrameMenuGroup("Gimmick")]
        public ReFrameDeleteEntry jumpDash = new() { Enabled = false, Value = 0f };

        // ---- AFK ----

        [ReFrameMenuGroup("Locomotion")]
        [ReFrameDelete("plium_AFK", ReFrameParameterType.Bool)]
        [ReFrameLabel("AFK (メニュー)")]
        [ReFrameRowOrder(8)]
        public ReFrameDeleteEntry afk = new() { Enabled = false, Value = 0f };

        // VRChat 本体の AFK でも出るぬいぐるみと演出。
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("POTECHI_AFK")]
        [ReFrameLabel("AFK のぬいぐるみ")]
        [ReFrameRowOrder(9)]
        [ReFrameQuestParticleMaterial("POTECHI_AFK/AFK Particle", Recursive = true)]
        public ReFrameDeleteEntry afkPotechi = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("VRCEmote", ReFrameParameterType.Int)]
        [ReFrameValueLocked(0f)]
        [ReFrameLabel("エモート (VRCEmote)")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(10)]
        public ReFrameDeleteEntry vrcEmote = new() { Enabled = false, Value = 0f };

        // ---- ロコモーション (公式 plium_Loco) ----
        // メニューのボタンが Change_* を書き、SetView_* 層が *_M へ写してから 0 に戻す。Locomotion の BlendTree は *_M で姿勢を選ぶ。
        // 消すときは選んだポーズ (かぐやと同じく焼き込み) に Change_* と *_M の両方を固定して、写す層ごと外す。
        // BlendTree のしきい値は 0.01 から始まるので 0 は 1 番と同じ。公式の初期値も 1 番 (0.01)。
        // Change_* を読むのは SetView_* だけなので、*_M と同じ値で固定してよい。

        [ReFrameDelete("Change_Standing", ReFrameParameterType.Float)]
        [ReFrameDelete("Change_Standing_M", ReFrameParameterType.Float)]
        [ReFrameValueChoices("1=0.01", "2=0.02", "3=0.03", "4=0.04", "5=0.05", "6=0.06", "7=0.07", "8=0.08")]
        [ReFrameCollapseBlendTree]
        [ReFrameDeleteLayer("SetView_Standing")]
        [ReFrameLabel("立ちポーズ")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(0)]
        public ReFrameDeleteEntry standingPoses = new() { Enabled = false, Value = 0.01f };

        [ReFrameDelete("Change_Crouching", ReFrameParameterType.Float)]
        [ReFrameDelete("Change_Crouching_M", ReFrameParameterType.Float)]
        [ReFrameValueChoices("1=0.01", "2=0.02", "3=0.03", "4=0.04", "5=0.05", "6=0.06", "7=0.07", "8=0.08")]
        [ReFrameCollapseBlendTree]
        [ReFrameDeleteLayer("SetView_Crouching")]
        [ReFrameLabel("しゃがみポーズ")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(1)]
        public ReFrameDeleteEntry crouchingPoses = new() { Enabled = false, Value = 0.01f };

        // ポーズを消すと Prone のサブメニューには Mirror (全姿勢の左右反転) だけが残るので、Mirror を Locomotion へ移す。
        [ReFrameMenuRemove("Locomotion/Prone", Keep = new[] { "Mirror" })]
        [ReFrameDelete("Change_Prone", ReFrameParameterType.Float)]
        [ReFrameDelete("Change_Prone_M", ReFrameParameterType.Float)]
        [ReFrameValueChoices("1=0.01", "2=0.02", "3=0.03", "4=0.04", "5=0.05", "6=0.06", "7=0.07")]
        [ReFrameCollapseBlendTree]
        [ReFrameDeleteLayer("SetView_Prone")]
        [ReFrameLabel("伏せポーズ")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(2)]
        public ReFrameDeleteEntry pronePoses = new() { Enabled = false, Value = 0.01f };

        [ReFrameDelete("Floating", ReFrameParameterType.Float)]
        [ReFrameDelete("Floating_M", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameCollapseBlendTree]
        [ReFrameDeleteLayer("SetView_Floating")]
        // 浮遊は姿勢ではなくモード (Floating_M > 0 の間ずっと浮く、Reset = -1 で戻る) なので、焼き込まずに消すだけ。
        [ReFrameLabel("浮遊ポーズ")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(3)]
        public ReFrameDeleteEntry floatingPoses = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Change_Mirror", ReFrameParameterType.Bool)]
        [ReFrameLabel("ポーズの左右反転")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(4)]
        public ReFrameDeleteEntry poseMirror = new() { Enabled = false, Value = 0f };

        // 1 = ジャンプ・落下のモーションを出さない (既定)。
        [ReFrameDelete("Jump_Cancel", ReFrameParameterType.Bool)]
        [ReFrameLabel("ジャンプモーション OFF")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(5)]
        public ReFrameDeleteEntry jumpCancel = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Move_Animation_Off", ReFrameParameterType.Bool)]
        [ReFrameLabel("移動モーション OFF (Hover)")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(6)]
        public ReFrameDeleteEntry moveAnimationOff = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Leg_Fixed", ReFrameParameterType.Bool)]
        [ReFrameLabel("足固定")]
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameRowOrder(7)]
        public ReFrameDeleteEntry legFixed = new() { Enabled = false, Value = 0f };

        // 書き手の無いパラメーター (メニュー・EP・Driver・Contact のどれにも無く、常に既定値) の分岐を畳む。
        // Change_All_Reset: All_Reset 層と SetView_* の VR 側のリセット。Mogumogu: 公式 FX の手の層。Chiyu Meat: Gesture の右手。
        [ReFrameLabel("書き手の無い分岐 (常に削除)")]
        [ReFrameValueLocked(0f)]
        [ReFrameCutUndrivenTransitions("Change_All_Reset", EvaluateAsFixed = true)]
        [ReFrameCutUndrivenTransitions("Mogumogu", EvaluateAsFixed = true)]
        [ReFrameCutUndrivenTransitions("Chiyu Meat", EvaluateAsFixed = true)]
        public ReFrameDeleteEntry leftovers = new() { Enabled = true, Value = 0f };
    }
}
