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
        public ReFrameDeleteEntry onepiece = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Chiyu_Socks", ReFrameParameterType.Bool)]
        [ReFrameLabel("ソックス")]
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

        // 0 (胸小さく 100) / 0.5 (どちらも 0) / 1 (胸大きく 100) の 1D BlendTree。
        [ReFrameDelete("Breast_Size", ReFrameParameterType.Float)]
        [ReFrameApplyToAvatar]
        [ReFrameLabel("胸の大きさ")]
        public ReFrameDeleteEntry breastSize = new() { Enabled = false, Value = 0.5f };

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
        public ReFrameDeleteEntry jumpDash = new() { Enabled = false, Value = 0f };

        // ---- AFK ----

        [ReFrameMenuGroup("Locomotion")]
        [ReFrameDelete("plium_AFK", ReFrameParameterType.Bool)]
        [ReFrameLabel("AFK (メニュー)")]
        public ReFrameDeleteEntry afk = new() { Enabled = false, Value = 0f };

        // VRChat 本体の AFK でも出るぬいぐるみと演出。
        [ReFrameMenuGroup("Locomotion")]
        [ReFrameValueLocked(0f)]
        [ReFrameDeleteObject("POTECHI_AFK")]
        [ReFrameLabel("AFK のぬいぐるみ")]
        public ReFrameDeleteEntry afkPotechi = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("VRCEmote", ReFrameParameterType.Int)]
        [ReFrameValueLocked(0f)]
        [ReFrameLabel("エモート (VRCEmote)")]
        public ReFrameDeleteEntry vrcEmote = new() { Enabled = false, Value = 0f };

        // ---- ロコモーション (公式 plium_Loco) ----
        // メニューのボタンが Change_* を書き、SetView_* 層が *_M へ写してから 0 に戻す。Locomotion の BlendTree は *_M で姿勢を選ぶ。
        // 消すときは通常の姿勢 (0) に固定して、写す層ごと外す。

        [ReFrameDelete("Change_Standing", ReFrameParameterType.Float)]
        [ReFrameDelete("Change_Standing_M", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameCollapseBlendTree]
        [ReFrameDeleteLayer("SetView_Standing")]
        [ReFrameLabel("立ちポーズ")]
        public ReFrameDeleteEntry standingPoses = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Change_Crouching", ReFrameParameterType.Float)]
        [ReFrameDelete("Change_Crouching_M", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameCollapseBlendTree]
        [ReFrameDeleteLayer("SetView_Crouching")]
        [ReFrameLabel("しゃがみポーズ")]
        public ReFrameDeleteEntry crouchingPoses = new() { Enabled = false, Value = 0f };

        // ポーズを消すと Prone のサブメニューには Mirror (全姿勢の左右反転) だけが残るので、Mirror を Locomotion へ移す。
        [ReFrameMenuRemove("Locomotion/Prone", Keep = new[] { "Mirror" })]
        [ReFrameDelete("Change_Prone", ReFrameParameterType.Float)]
        [ReFrameDelete("Change_Prone_M", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameCollapseBlendTree]
        [ReFrameDeleteLayer("SetView_Prone")]
        [ReFrameLabel("伏せポーズ")]
        public ReFrameDeleteEntry pronePoses = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Floating", ReFrameParameterType.Float)]
        [ReFrameDelete("Floating_M", ReFrameParameterType.Float)]
        [ReFrameValueLocked(0f)]
        [ReFrameCollapseBlendTree]
        [ReFrameDeleteLayer("SetView_Floating")]
        [ReFrameLabel("浮遊ポーズ")]
        public ReFrameDeleteEntry floatingPoses = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Change_Mirror", ReFrameParameterType.Bool)]
        [ReFrameLabel("ポーズの左右反転")]
        public ReFrameDeleteEntry poseMirror = new() { Enabled = false, Value = 0f };

        // 1 = ジャンプ・落下のモーションを出さない (既定)。
        [ReFrameDelete("Jump_Cancel", ReFrameParameterType.Bool)]
        [ReFrameLabel("ジャンプモーション OFF")]
        public ReFrameDeleteEntry jumpCancel = new() { Enabled = false, Value = 1f };

        [ReFrameDelete("Move_Animation_Off", ReFrameParameterType.Bool)]
        [ReFrameLabel("移動モーション OFF (Hover)")]
        public ReFrameDeleteEntry moveAnimationOff = new() { Enabled = false, Value = 0f };

        [ReFrameDelete("Leg_Fixed", ReFrameParameterType.Bool)]
        [ReFrameLabel("足固定")]
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
